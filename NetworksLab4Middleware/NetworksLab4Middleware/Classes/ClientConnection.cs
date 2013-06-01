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


        public IPAddress EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }

        /// <summary>
        /// Setup and connect to the endpoint server
        /// </summary>
        public void Start()
        {
            ClientStateSaver clientState = new ClientStateSaver();
            clientState.clientSocket = ConnectSock();
            serverState.clientState = clientState;

            Thread receiveThread = new Thread(delegate()
                {
                    ReceiveHandler(serverState);
                });

            receiveThread.Start();
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

        // begins sending message on to the endPoint
        public void BeginTransmission()
        {
            // Create necessary threads to handle client connection
            serverState.clientState.clientThread = new Thread(delegate()
                {
                    ThraedSendFunction(serverState);
                });

            // Start threads listening and sending
            serverState.clientState.clientThread.Start();

            //receiveThread.Join();
            //serverState.clientState.clientThread.Join();
        }

        /// <summary>
        /// Handles the connection to the endpoint server
        /// </summary>
        /// <param name="clientState">
        /// CleintStateSaver object.
        /// </param>
        private void ThraedSendFunction(ServerStateSaver serverState)
        {
            // TODO: Build the connection handling logic
            // Be sure to use the client thread/states inside serverState
        }

        /// <summary>
        /// Receiving side connection handler
        /// </summary>
        /// <param name="clientState">
        /// takes the ClientStateSaver being used by the hostconnection
        /// server to keep the messages compiled in the same location
        /// </param>
        private void ReceiveHandler(ServerStateSaver serverState)
        {
            // TODO: Build receiving logic
            // Be sure to use the client thread/states inside serverState
        }
    }
}
