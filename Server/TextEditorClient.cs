using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace IdeBridge
{

    public class TextEditorClient : Client
    {
        private MainPanel _form = null;
        Backend _backend;
        Configuration _config = Configuration.Get();
        string _compilerPath = "";

        public TextEditorClient(Listener listener, TcpClient client)
            : base(listener, client)
        {
            _backend = Backend.Get;
            _compilerPath = Path.GetDirectoryName(Application.ExecutablePath);
            _compilerPath += "/IdeBridgeCompiler.exe";
            _compilerPath = _compilerPath.Replace("\\", "/");
            Logger.Info( "compilerPath: " + _compilerPath);
            //_backend.MonitorActivation();
        }

        protected void SendSolutionList(string filename)
        {
            var list = new List<string>();
            string path = Path.GetDirectoryName(filename);
            while (path != "" && path != null)
            {
                foreach (string file in Directory.GetFiles(path, "*.sln", SearchOption.TopDirectoryOnly))
                {
                    list.Add(file.Replace('\\', '/'));
                }
                path = Path.GetDirectoryName(path);
            }

            if (list.Count == 0)
            {
                WriteMessage("No solution files found");
                return;
            }

            string command = "(ide-bridge-select-solution '(";
            foreach (string file in list)
            {
                command += "\"" + file + "\" ";
            }
            command += "))";
            Write(command);
        }

        protected override void ProcessMessages(string[] messages)
        {
            if (_form == null) _form = MainPanel.Get;

            _form.Invoke((MethodInvoker)delegate
                         {
                             foreach (var m in messages) ProcessCommand(new Command(m));
                         });
        }

        public void LaunchCompilation(string command)
        {
            Logger.Info("(compile \"" + _compilerPath + " " + command + "\")");
            Write("(compile \"" + _compilerPath + " " + command + "\")");
        }

        public void SendText(string text)
        {
            if (text != "")
            {
                Write(string.Format("(ide-bridge-insert-candidate \"{0}\")", text));
            }
        }

        public void GoToDefinition()
        {
            _backend.Message = "";
            _backend.GoToDefinition();
        }

        public void Test()
        {
            _backend.Message = "";
            _backend.Test();
        }

        public void GoToNextLocation()
        {
            _backend.Message = "";
            _backend.GoToNextLocation();
            GoTo();
        }

        public void WriteMessage()
        {
            if( _backend.Message != "")
            {
                Write( "(message \"" + _backend.Message + "\")");
            }
            _backend.Message = "";
        }

        public void WriteMessage(string message)
        {
             Write("(message \"" + message + "\")");
        }

        public void CancelCompletion()
        {
            Write( "(ide-bridge-completion-cleanup)");
        }

        public void GoTo()
        {
            var message = "";
            if( _backend.Message != "")
            {
                message = "(message \"" + _backend.Message + "\")";
            }

            var txt = string.Format("(progn (find-file \"{0}\") (goto-line {1}) (move-to-column {2}) {3} )",
                                    _backend.FileName.Replace( "\\", "/"),
                                    _backend.Line,
                                    _backend.Column,
                                    message);

            Write(txt);
            _backend.Message = "";
        }

        public void InitCompleteContext()
        {
            Write("(ide-bridge-init-complete-context)");
        }

        // In GUI Thread :
        private void ProcessCommand(Command command)
        {
            try
            {
                string[] args;
                _backend.Client = this;
                switch (command.Name)
                {
                    case "buffer":
                        _backend.Buffer = command.Arguments;
                        break;
                    case "go-to-definition":
                        args = command.Arguments.Split(new char[] { '|' });
                        _backend.FileName = args[0];
                        _backend.Line = int.Parse(args[2]);
                        // -1 (emacs-point) start at 1, -1 (emacs-line) start at 1 => -2 :
                        _backend.Offset = int.Parse(args[1]) + _backend.Line - 2;
                        _backend.Column = int.Parse(args[3]);
                        GoToDefinition();
                        break;
                    case "go-to":
                        args = command.Arguments.Split(new char[] { '|' });
                        _backend.FileName = args[0];
                        _backend.Line = int.Parse(args[2]);
                        // -1 (emacs-point) start at 1, -1 (emacs-line) start at 1 => -2 :
                        _backend.Offset = int.Parse(args[1]) + _backend.Line - 2;
                        _backend.Column = int.Parse(args[3]);
                        _backend.GoTo();
                        break;
                    case "go-to-next-location":
                        GoToNextLocation();
                        break;
                    case "test":
                        args = command.Arguments.Split(new char[] { '|' }, 2);
                        _backend.FileName = args[0];
                        _backend.Offset = int.Parse(args[1]);
                        Test();
                        break;
                    case "list-solutions":
                        SendSolutionList(command.Arguments);
                        break;
                    case "set-solution":
                        _backend.Message = "";
                        _config.Solution = command.Arguments;
                        _config.Save();
                        _backend.LoadSolution();
                        WriteMessage();
                        break;
                    case "complete":
                        args = command.Arguments.Split(new char[] { '|' }, 9);

                        _backend.ErrorDetected = false;
                        _backend.X = int.Parse(args[0]) + _config.XOffset;
                        _backend.Y  = int.Parse(args[1]) + _config.YOffset;
                        _backend.CharHeight = int.Parse(args[2]);
                        _backend.FileName = args[3];
                        _backend.Line = int.Parse(args[5]);
                        // -1 (emacs-point) start at 1, -1 (emacs-line) start at 1 => -2 :
                        _backend.Offset = int.Parse(args[4]) + _backend.Line - 2;
                        _backend.Column = int.Parse(args[6]);
                        _backend.OnlyContext = (args[7] == "1");
                        _backend.PreSelection = args[8];
                        _backend.Client = this;

                        _backend.Message = "";
                        _backend.Complete();
                        WriteMessage();
                        if( _backend.ErrorDetected )
                        {
                            Write("(ide-bridge-completion-cleanup)");
                        }
                        break;
                    case "build":
                        _backend.Build(command.Arguments);
                        break;
                    case "rebuild":
                        _backend.ReBuild(command.Arguments);
                        break;
                    case "build-by-filename":
                        _backend.BuildByFileName(command.Arguments);
                        break;
                    case "rebuild-by-filename":
                        _backend.ReBuildByFileName(command.Arguments);
                        break;
                    case "hide":
                        _backend.Hide();
                        break;
                    case "filter":
                        _backend.SetText(command.Arguments);
                        break;
                    case "next-line":
                        _backend.Down();
                        break;
                    case "previous-line":
                        _backend.Up();
                        break;
                    case "next-page":
                        _backend.PageDown();
                        break;
                    case "previous-page":
                        _backend.PageUp();
                        break;
                    case "exception":
                        break;
                    case "insert":
                        SendText(_backend.Insert());
                        break;
                    case "insert-spc":
                        SendText(_backend.Insert() + " ");
                        break;
                    case "save-all":
                        Listener.WriteOther(this, "(save-some-buffers 't)");
                        Write("Bye\n");
                        break;
                    default:
                        Logger.Error("Client Error:unknow command:\n\n" + command.Name + ":" + command.Arguments);
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Exception(e);
                try { WriteMessage(e.Message); }
                catch (Exception) { }
            }
            finally
            {
                try
                {
                    _backend.CleanUp();
                }
                catch (Exception e)
                {
                    Logger.Exception(e);
                }
            }
        }
    }
}
