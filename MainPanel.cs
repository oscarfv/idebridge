using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace IdeBridge
{
    public partial class MainPanel : Form
    {
        private Configuration _config = Configuration.Get();
        private Listener _listener = null;

        public static MainPanel Get { get; set; }

        public MainPanel()
        {
            InitializeComponent();

            outputBox.Focus();

            StartPosition = FormStartPosition.CenterScreen;
            Logger.Output = outputBox;

            Get = this;

            configurationGrid.SelectedObject = _config;

            _listener = new Listener();

            _listener.OnClientListChange += RefreshClients;

            Start();
            outputBox.Focus();
        }

        public void RefreshClients()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { RefreshClients(); });
                return;
            }

            clients.Items.Clear();
            foreach (var client in _listener.Clients)
            {
                clients.Items.Add(client);
            }
        }


        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void ServerForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Hide();
            }
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon.Visible = false;
            _listener.Stop();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void Start()
        {
            try
            {
                startButton.Enabled = false;

                _listener.Start(_config.PortNumber);

                stopButton.Enabled = true;

                Logger.Info("the server is started");
            }
            catch (System.Exception exc1)
            {
                startButton.Enabled = true;
                Logger.Exception(exc1);
            }
        }

        private void Stop()
        {
            try
            {
                stopButton.Enabled = false;

                _listener.Stop();

                startButton.Enabled = true;
                Logger.Info("the server is stopped");
            }
            catch (System.Exception exc1)
            {
                stopButton.Enabled = true;
                Logger.Exception(exc1);
            }
        }

        private void configurationGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            _config.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SharpDevBackend.Instance.Test();
        }
    }
}
