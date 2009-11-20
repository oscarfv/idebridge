﻿﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Collections;
using ICSharpCode.SharpDevelop.Project;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.TextEditor.Gui.InsightWindow;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.Core.Services;
using ICSharpCode.Core;
using ICSharpCode.TextEditor;
using ICSharpCode.SharpDevelop.DefaultEditor.Gui.Editor;

namespace IdeBridge
{
    class WatchedFile
    {
        public string FileName;
        public DateTime LastWriteTime;

        public WatchedFile(string fileName)
        {
            FileName = fileName;
            LastWriteTime = Directory.GetLastWriteTime(FileName);
        }

        public bool IsUpToDate()
        {
            var newLastWriteTime = Directory.GetLastWriteTime(FileName);
            if (newLastWriteTime != LastWriteTime)
            {
                LastWriteTime = Directory.GetLastWriteTime(FileName);
                return false;
            }
            return true;
        }

    }

    internal class DummyProgressMonitor : IProgressMonitor
    {
        int workDone;
        string taskName;
        bool showingDialog;

        public int WorkDone
        {
            get { return workDone; }
            set { workDone = value; }
        }

        public string TaskName
        {
            get { return taskName; }
            set { taskName = value; }
        }

        public void BeginTask(string name, int totalWork, bool allowCancel)
        {
            taskName = name;
            workDone = 0;
        }

        public void Done()
        {
        }

        public bool IsCancelled
        {
            get { return false; }
        }

        public bool ShowingDialog
        {
            get { return showingDialog; }
            set { showingDialog = value; }
        }

        public event EventHandler Cancelled { add { } remove { } }
    }


    sealed class ViewSink : IBuildFeedbackSink
    {
        Client _client;
        public ViewSink(Client client)
        {
            _client = client;
        }

        public void ReportError(BuildError error)
        {
            Logger.Info("===> Error: " + error.ToString());
        }

        public void ReportMessage(string message)
        {
            _client.Write(ICSharpCode.Core.StringParser.Parse(message));
        }

        public void Done(bool success)
        {
            _client.Write("Build Done!");
        }
    }

    public class SharpDevBackend : Backend
    {
        protected Configuration _config = Configuration.Get();
        Workbench _workBench;
        TextEditorClient _client = null;
        List<WatchedFile> _watchedFiles = new List<WatchedFile>();
        List<WatchedFile> _watchedProjects = new List<WatchedFile>();
        IdeBridgeTextAreaControl _textArea;

        public static SharpDevBackend Instance { get; set; }

        public override Client Client
        {
            get { return _client; }
            set { _client = value as TextEditorClient; }
        }

        public SharpDevBackend()
        {
            Instance = this;
            ServiceManager.LoggingService = new LogginService();

            X = Y = 0;
            PreSelection = "";

            _workBench = new Workbench(MainPanel.Get);
            IdeBridgeManager.Init();
            WorkbenchSingleton.InitializeWorkbench(_workBench, new WorkbenchLayout());

            ProjectService.SolutionLoaded += new EventHandler<SolutionEventArgs>(ProjectService_SolutionLoaded);
            MainPanel.Get.Activated += new EventHandler(OnActivated);

            var panel = WorkbenchSingleton.Workbench.GetPad(typeof(CompilerMessageView)).PadContent.Control;
            panel.Dock = DockStyle.Fill;
            MainPanel.Get.SharpPage.Controls.Add(panel);

            //LoadSolution();

            ParserService.StartParserThread();

            _textArea = new IdeBridgeTextAreaControl(this);

            // var factory = new DocumentFactory();
            // Document = factory.CreateDocument();
            // Document.FormattingStrategy = new CSharpFormattingStrategy();
        }

        void OnActivated(object sender, EventArgs e)
        {
            MainPanel.Get.Activated -= OnActivated;
            LoadSolution();
        }

        protected void CallbackMethod(BuildResults results)
        {

        }


        public override void BuildByFileName(string fileName)
        {
            FindProjectAndBuild("build", fileName);
        }
        public override void ReBuildByFileName(string fileName)
        {
            FindProjectAndBuild("rebuild", fileName);
        }

        public void FindProjectAndBuild(string target, string fileName)
        {
            string normalizedFileName = FileUtility.NormalizePath(fileName).ToLower();
            foreach (var project in ProjectService.OpenSolution.Projects)
            {
                foreach(var item in project.Items)
                {
                    //Logger.Info("item: '" + item.FileName + "' and :'" + normalizedFileName + "'");
                    if( item.FileName.ToLower() == normalizedFileName )
                    {
                        _client.LaunchCompilation(target + " " + project.Name);
                        return;
                    }
                }
            }
            Message = "No projects found for " + fileName;
            _client.WriteMessage();
        }

        public override void Build(string projectName)
        {
            StartBuild(BuildTarget.Build, projectName);
        }

        public override void ReBuild(string projectName)
        {
            StartBuild(BuildTarget.Rebuild, projectName);
        }

        public void StartBuild(BuildTarget target, string projectName)
        {
            IBuildable buildable = null;

            if (projectName == "")
            {
                buildable = ProjectService.OpenSolution;
            }
            else
            {
                foreach (var project in ProjectService.OpenSolution.Projects)
                {
                    if (project.Name == projectName)
                    {
                        buildable = project;
                        break;
                    }
                }
            }

            if (buildable != null)
            {
                BuildEngine.StartBuild(buildable, new BuildOptions(target, CallbackMethod),
                           new ViewSink(Client), new DummyProgressMonitor());
            }
            else
            {
                Client.Write("Project \"" + projectName + "\" not found!\n");
                Client.Write("Build Done!");
            }

        }

        public override void LoadSolution()
        {
            _watchedProjects.Clear();
            _watchedFiles.Clear();
            ProjectService.CloseSolution();

            if (!File.Exists(_config.Solution))
            {
                Message = "Solution file not found";
                Logger.Error(Message);
                return;
            }

            ProjectService.LoadSolution(_config.Solution);
        }

        void ProjectService_SolutionLoaded(object sender, SolutionEventArgs e)
        {
            Logger.Info("Solution " + e.Solution.Name + " loaded!");

            // Logger.Info(e.Solution.Name);
            _watchedProjects.Add(new WatchedFile(e.Solution.FileName));
            Logger.Info("Solution: " + Directory.GetLastWriteTime(e.Solution.FileName));
            foreach (IProject p in e.Solution.Projects)
            {
                _watchedProjects.Add(new WatchedFile(p.FileName));
                Logger.Info("Projects: " + p.FileName + " : " + Directory.GetLastWriteTime(p.FileName));
                foreach (var obj in p.Items)
                {
                    if (obj.ItemType == ItemType.Compile)
                    {
                        _watchedFiles.Add(new WatchedFile(obj.FileName));
                    }
                }
            }
        }

        public void ThrowError(string error)
        {
            Logger.Error(error + "\n");
            throw new Exception(error);
        }

        public override void MonitorActivation()
        {
        }

        #region GoToDefinition
        public override void GoToDefinition()
        {
            Logger.Info("Buffer[Offset] : " + Buffer.Substring(Offset, 10));
            IExpressionFinder expressionFinder = ParserService.GetExpressionFinder(FileName);
            if (expressionFinder == null)
            {
                Logger.Info("expressionFinder == null");
                return;
            }
            ExpressionResult expression = expressionFinder.FindFullExpression(Buffer, Offset);
            if (expression.Expression == null || expression.Expression.Length == 0)
            {
                Logger.Info("expression.Expression == null");
                return;
            }
            ResolveResult result = ParserService.Resolve(expression, Line, Column, FileName, Buffer);
            if (result != null)
            {
                FilePosition pos = result.GetDefinitionPosition();
                if (pos.IsEmpty == false)
                {
                    JumpToFilePosition(pos.FileName, pos.Line, pos.Column);
                    return;
                }
                else
                {
                    Logger.Info("pos.IsEmpty == true");
                }
            }
            Message = "Target not found!";
            _client.WriteMessage();
        }

        public override void GoToNextLocation()
        {
        }

        public override void GoTo()
        {
            GotoDialog.ShowSingleInstance();
        }

        public void JumpTo(int line, int column)
        {
            JumpToFilePosition(FileName, line, column);
        }

        public void JumpToFilePosition(string fileName, int line, int column)
        {
            FileName = fileName;
            Line = line;
            Column = column;
            _client.GoTo();
        }
        #endregion GoToDefinition

        #region Test
        public override void Test()
        {
            Build("");
        }
        #endregion Test

        #region Intellisense Completion Stuff :

        //Char _firstChar = ' ';

        bool TrySpecializedCompletion(int colum, int line, int offset)
        {
            if( colum < 0 || line < 0 ) return false;

            var loc = new TextLocation(colum, line);
            _textArea.ActiveTextAreaControl.Caret.Position = loc;

            // Message = "offset1 = '" + Offset + "' offset2 = '" + _textArea.ActiveTextAreaControl.Caret.Offset + "'";
            // Logger.Info(Message);

            Logger.Info( "WordBeforCaret1 : '" + _textArea.GetWordBeforeCaret() + "'");

            foreach (ICodeCompletionBinding ccBinding in SharpDevelopTextAreaControl.CodeCompletionBindings) {
                if (ccBinding.HandleKeyPress(_textArea, Buffer[offset]))
                {
                    Logger.Info( "Specialized completion !!");
                    return true;
                }
            }

            if( _textArea.IsCompleting )
            {
                Logger.Info( "Specialized completion !!");
                return true;
            }

            Logger.Info( "WordBeforCaret2 : '" + _textArea.GetWordBeforeCaret() + "'");

            return false;
        }

        public override void Complete()
        {
            try
            {
                _textArea.FileName = FileName;
                _textArea.Text = Buffer;

                if (TrySpecializedCompletion(Column - 2, Line - 1, Offset - 2)) return;
                if (TrySpecializedCompletion(Column - 1, Line - 1, Offset - 1)) return;

                if (OnlyContext) return;

                var loc = new TextLocation(Column, Line - 1);
                _textArea.ActiveTextAreaControl.Caret.Position = loc;

                CtrlSpaceCompletionDataProvider provider = new CtrlSpaceCompletionDataProvider();
                provider.AllowCompleteExistingExpression = true;
                _textArea.ShowCompletionWindow(provider, '\0');

                // //string txt = _textArea.ActiveTextAreaControl.

                // Message = "offset1 = '" + Offset + "' offset2 = '" + _textArea.ActiveTextAreaControl.Caret.Offset + "'";
                // Logger.Info(Message);

                // _client.WriteMessage();
            }
            finally
            {
                if (_textArea.IsInInsight)
                {
                    _client.SetInsightContext();
                }
                else if( _textArea.IsInCompletion )
                {
                    _client.SetCompletionContext();
                }
            }
        }

        public override void Insight()
        {
            throw new NotImplementedException();
        }

        private void StopCompleting()
        {
            _textArea.StopCompleting();
        }

        public override void Hide()
        {
            StopCompleting();
        }

        public override void CleanUp()
        {
            foreach (var watched in _watchedFiles)
            {
                if (!watched.IsUpToDate())
                {
                    Logger.Info("Enqueue for reparsing :" + watched.FileName);
                    ParserService.StartAsyncParse(watched.FileName, ParserService.GetParseableFileContent(watched.FileName));
                }
            }

            bool reloadSolution = false;
            foreach (var watched in _watchedProjects)
            {
                if (!watched.IsUpToDate())
                {
                    Logger.Info("Not up to date: " + watched.FileName + " : " + Directory.GetLastWriteTime(watched.FileName));
                    reloadSolution = true;
                    break;
                }
            }

            if (reloadSolution)
            {
                LoadSolution();
            }
        }

        public override string Insert()
        {
            int offset1 = _textArea.ActiveTextAreaControl.Caret.Offset;
            var result = _textArea.DoCompletion();
            if( result.Length > 0 && result[0] == ' ' )
            {
                result = result.Substring(1,result.Length-1);
            }
            return result;
        }

        public override void PageDown()
        {
            _textArea.PageDown();
        }

        public override void PageUp()
        {
            _textArea.PageUp();
        }

        public override void Down()
        {
            _textArea.Down();
        }

        public override void Up()
        {
            _textArea.Up();
        }

        public override void SetText(string text)
        {
            _textArea.SetText(text);
        }
        #endregion
    }
}
