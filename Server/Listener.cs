using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;


namespace IdeBridge
{
    public class Listener
    {
        protected IPAddress _ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
        protected TcpListener _listener = null;
        private   List<Client> _clients = new List<Client>();
        protected bool _started = false;
        protected bool _closing = false;
        protected int _portNumber = 0;

        public delegate void ClientsListChanged(); 
        public ClientsListChanged OnClientListChange;

        public Client[] Clients {
            get {
                Client [] result;
                lock(_clients) result = _clients.ToArray();
                return result;
            }
        }

        public void Start(int port)
        {
            try
            {
                _portNumber = port;

                _listener = new TcpListener(_ipAddress, _portNumber);

                _listener.Start();

                _listener.BeginAcceptTcpClient(new AsyncCallback(DoAcceptTcpClientCallback), null);
                _started = true;
            }
            catch(Exception e)
            {
                Logger.Error( e.Message);
            }
        }

        public void Disconnect(Client client)
        {
            lock(_clients)
            {
                _clients.Remove(client);
            }
            if( OnClientListChange != null ) OnClientListChange();
        }

        protected void DoAcceptTcpClientCallback(IAsyncResult ar)
        {
            if( _closing ) return;

            var client = CreateClient(_listener.EndAcceptTcpClient(ar));
            lock(_clients) _clients.Add(client);
            client.Start();

            if( OnClientListChange != null ) OnClientListChange();

            _listener.BeginAcceptTcpClient(new AsyncCallback(DoAcceptTcpClientCallback), null);
        }

        public void WriteOther(Client sender, string message)
        {
            Client[] clients = null;
            lock(_clients) clients = _clients.ToArray();

            foreach(var client in clients)
            {
                if( client == sender ) continue;
                client.Write( message);
            }
        }

        protected Client CreateClient(TcpClient client)
        {
            return new TextEditorClient(this, client);
        }

        public void Stop()
        {
            if (!_started) return;

            _closing = true;

            var tmpTcp = new TcpClient();
            tmpTcp.Connect( _ipAddress, _portNumber);
            var stream = tmpTcp.GetStream();
            var writer = new StreamWriter(stream);
            writer.Write("close this!!");
            writer.Close();
            stream.Close();
            tmpTcp.Close();

            _listener.Stop();
            _started = false;
        }
    }
}
