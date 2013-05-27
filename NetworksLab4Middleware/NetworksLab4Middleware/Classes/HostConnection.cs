using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace NetworksLab4Middleware.Classes
{
    class HostConnection
    {
        // Private class constants
        private const int BUFFER_SIZE = 256;
        private const int LENGTH_BITS = 2;
        private const int PORT = 2605;
        private const int MAX_MESSAGES = 5000;

        // Private class variables
        private Socket sock = null;
        private int wirelessNicIndex = 0;
        private System.Windows.Forms.RichTextBox testDataTextbox;
        private Thread acceptThread = null;
        private string localServerIP = string.Empty;


        /// <summary>
        /// Non-default constructor. Used to set up
        /// server variables to begin server thread
        /// management.
        /// </summary>
        /// <param name="localIP">
        /// IP Address of the network device being used
        /// to host server.
        /// </param>
        /// <param name="wirelessNicIndex">
        /// Network device index value to set for server
        /// </param>
        /// <param name="testDataTextbox">
        /// RichTextbox to print test data to for debugging work.
        /// </param>
        public HostConnection(int wirelessNicIndex, System.Windows.Forms.RichTextBox testDataTextbox)
        {
            //this.localIP = localIP;
            this.wirelessNicIndex = wirelessNicIndex;
            this.testDataTextbox = testDataTextbox;
        }

        /// <summary>
        /// Starts accept thread and begins
        /// listening for a connection on 
        /// port 2605
        /// </summary>
        public void Start()
        {
            SetSock();
            acceptThread = new Thread(AcceptConnections);
            acceptThread.IsBackground = true;
            acceptThread.Start();
        }

        /// <summary>
        /// Set up the socket on host side
        /// </summary>
        private void SetSock()
        {
            // Get local information
            IPHostEntry localIP = Dns.GetHostEntry(Dns.GetHostName());
            IPEndPoint localEndPoint = new IPEndPoint(localIP.AddressList[wirelessNicIndex], PORT);
            localServerIP = localEndPoint.ToString().Split(':')[0];

            // Set up host socket
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sock.Bind(localEndPoint);
            sock.Listen((int)SocketOptionName.MaxConnections);
        }

        private void AcceptConnections()
        {
            Socket socket = null;

            try
            {

            }
            catch (Exception e)
            {
                testDataTextbox.Text += "ERROR! " + e.Message;
            }
        }
    }
}
