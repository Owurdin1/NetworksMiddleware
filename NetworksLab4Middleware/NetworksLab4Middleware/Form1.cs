using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;

namespace NetworksLab4Middleware
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
            IPHostEntry localIP = Dns.GetHostEntry(Dns.GetHostName());
            
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
            Classes.HostConnection server;

            if (server2IPTextbox.Text.Equals(String.Empty))
            { 
                server = new Classes.HostConnection(
                    deviceDropDown.SelectedIndex, 
                    server1IPTextbox.Text, 
                    Convert.ToInt32(msgCountTextbox.Text), 
                    Convert.ToInt32(paceTextbox.Text), 
                    testDataTextbox);
            }
            else
            {
                    server = new Classes.HostConnection(
                    deviceDropDown.SelectedIndex, 
                    server1IPTextbox.Text, 
                    server2IPTextbox.Text, 
                    Convert.ToInt32(msgCountTextbox.Text), 
                    Convert.ToInt32(paceTextbox.Text),
                    testDataTextbox);
            }

            server.Start();
        }

        private void finishButton_Click(object sender, EventArgs e)
        {
            server1IPTextbox.Text = "";
            server2IPTextbox.Text = "";
            myIPTextbox.Text = "";
            testDataTextbox.Text = "";
            deviceDropDown.SelectedIndex = 0;
            deviceListTextbox.Text = "";
            paceTextbox.Text = "";
        }
    }
}
