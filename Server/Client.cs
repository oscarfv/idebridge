using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace IdeBridge
{
    public class Command
    {
        public string Name;
        public string Arguments;

        public Command(string message)
        {
            string[] split = message.Split(new char[] { ':' }, 2);
            if (split.Length == 2)
            {
                Name = split[0];
                Arguments = split[1];
            }
            else
            {
                Name = "bad_command";
                Arguments = message;
            }
        }
    }

    public abstract class Client
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private Byte[] _data = new Byte[256];
        private Byte[] _writeBuffer = null;
        private string _inCompleteMessage = "";

        public Listener Listener = null;

        public static readonly char[] EndOfTrame = new char[] { '\a' };

        public override string ToString()
        {
            return "Client: " + _client.ToString();
        }

        public Client(Listener listener, TcpClient client)
        {
            Listener = listener;

            _client = client;
            _stream = client.GetStream();
        }

        public void Start()
        {
            try
            {
                _stream.BeginRead(_data, 0, _data.Length, new AsyncCallback(DoReadCallback), null);
            }
            catch (IOException)
            {
                OnDisconnection();
            }
        }

        private void DoReadCallback(IAsyncResult ar)
        {
            try
            {
                Int32 bytes = _stream.EndRead(ar);

                if (bytes != 0)
                {
                    ReadMessages(System.Text.Encoding.UTF8.GetString(_data, 0, bytes));
                    _stream.BeginRead(_data, 0, _data.Length, new AsyncCallback(DoReadCallback), null);
                }
                else
                {
                    OnDisconnection();
                }
            }
            catch (IOException)
            {
                OnDisconnection();
            }
        }

        protected void OnDisconnection()
        {
            Listener.Disconnect(this);
            _stream.Close();
            _client.Close();
            _stream = null;
            _client = null;
        }

        public void Close()
        {
            try
            {
                OnDisconnection();
            }
            catch (IOException)
            {
            }
        }

        private void ReadMessages(string message)
        {
            string[] messageList = message.Split(EndOfTrame);
            int length = messageList.Length;

            if (length == 0) return;

            if (_inCompleteMessage != "") messageList[0] = _inCompleteMessage + messageList[0];

            _inCompleteMessage = messageList[length - 1]; // will be "" if no incomplete message cf Split.

            // iterate over messageList ignoring lastEntry wich is incomplete.
            string[] messages = new string[length - 1];
            for (int i = 0; i < length - 1; i++)
            {
                if (messageList[i] == "exit:") { throw new IOException(); /* Close the connection */ }
                messages[i] = messageList[i];
            }

            ProcessMessages(messages);
        }

        protected abstract void ProcessMessages(string[] messages);

        public void Write(string message)
        {
            try
            {
                _writeBuffer = Encoding.UTF8.GetBytes(message);
                _stream.BeginWrite(_writeBuffer, 0, _writeBuffer.Length, new AsyncCallback(DoWriteCallback), null);
            }
            catch (IOException)
            {
                OnDisconnection();
            }
        }

        public void SendString(string message)
        {
            try
            {
                _writeBuffer = Encoding.UTF8.GetBytes(message + EndOfTrame[0].ToString());
                _stream.BeginWrite(_writeBuffer, 0, _writeBuffer.Length, new AsyncCallback(DoWriteCallback), null);
            }
            catch (IOException)
            {
                OnDisconnection();
            }
        }

        private void DoWriteCallback(IAsyncResult ar)
        {
            try
            {
                if (ar.AsyncState != null)
                {
                    _stream.EndWrite(ar);
                }
            }
            catch (IOException)
            {
                OnDisconnection();
            }
        }
    }
}
