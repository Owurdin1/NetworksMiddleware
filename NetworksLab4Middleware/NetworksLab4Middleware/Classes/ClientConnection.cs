using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace NetworksLab4Middleware.Classes
{
    class ClientConnection
    {
        // global constant variables
        private const int PORT = 2605;


        // global private variables
        private IPAddress endPoint;
        private Socket sock;
        private int pace = 0;
        private int msgCount = 0;
        private ServerStateSaver serverState = new ServerStateSaver();
        private List<byte[]> sendMessages = new List<byte[]>();
        private List<byte[]> recMessages = new List<byte[]>();

        // global public variables
        public System.Windows.Forms.RichTextBox testDataTextbox;

        /// <summary>
        /// sets or gets the pace of the client
        /// </summary>
        public int Pace
        {
            get { return pace; }
            set { pace = value; }
        }

        /// <summary>
        /// sets or gets the message count
        /// </summary>
        public int MsgCount
        {
            get { return msgCount; }
            set { msgCount = value; }
        }

        /// <summary>
        /// gets or sets the server state saver object
        /// </summary>
        public ServerStateSaver ServerState
        {
            get { return serverState; }
            set { serverState = value; }
        }

        /// <summary>
        /// Setup and connect to the endpoint server
        /// </summary>
        public void Start()
        {
            ClientStateSaver clientState = new ClientStateSaver();
            clientState.clientSocket = ConnectSock();
            clientState.clientStopWatch.Start();

            // Set up thread and begin handling connection
            clientState.clientThread = new Thread(delegate()
                {
                    ConnectionHandler(clientState);
                });

            clientState.clientThread.Start();
        }

        /// <summary>
        /// Setup the local client socket
        /// </summary>
        /// <returns>
        /// The socket that is connected to the endpoint
        /// </returns>
        private Socket ConnectSock()
        {
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sock.Connect(endPoint, PORT);
                sock.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
            }
            catch (Exception e)
            {
                testDataTextbox.Text += e.Message;
            }

            return sock;
        }

        /// <summary>
        /// Handles the connection to the endpoint server
        /// </summary>
        /// <param name="clientState">
        /// CleintStateSaver object.
        /// </param>
        private void ConnectionHandler(ClientStateSaver clientState)
        {
            // TODO: Build the connection handling logic
        }
    }
}
