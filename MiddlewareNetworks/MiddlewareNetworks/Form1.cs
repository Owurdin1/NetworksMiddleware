using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiddlewareNetworks
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void deviceQueryButton_Click(object sender, EventArgs e)
        {
            // Get local machine information
            System.Net.IPHostEntry localIP = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());

            string ipDevices = string.Empty;

            for (int i = 0; i < localIP.AddressList.Length; i++)
            {
                ipDevices += "Index: [" + i + "] " +
                    localIP.AddressList[i].ToString() + "\r\n";
            }

            deviceListTextbox.Text = ipDevices;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Classes.ServerRun sr;

            if (server2IPTextbox.Text.Equals(String.Empty))
            {
                sr = new Classes.ServerRun(
                    deviceDropDown.SelectedIndex,
                    server1IPTextbox.Text,
                    Convert.ToInt32(msgCountTextbox.Text),
                    Convert.ToInt32(paceTextbox.Text));
            }
            else
            {
                sr = new Classes.ServerRun(
                    deviceDropDown.SelectedIndex,
                    server1IPTextbox.Text,
                    server2IPTextbox.Text,
                    Convert.ToInt32(msgCountTextbox.Text),
                    Convert.ToInt32(paceTextbox.Text));
            }

            infoRichTextBox.Text += "Starting ServerRun\r\n";

            sr.Start();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            server1IPTextbox.Text = "";
            paceTextbox.Text = "";
            msgCountTextbox.Text = "";
            server2IPTextbox.Text = "";
            infoRichTextBox.Text = "";
        }
    }
}
