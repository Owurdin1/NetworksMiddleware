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

        // Private class variables
        private Socket sock = null;
        private int wirelessNicIndex = 0;
        private System.Windows.Forms.RichTextBox testDataTextbox;
        private Thread acceptThread = null;
        private IPAddress localServerIP;
        private IPAddress endPoint1;
        private IPAddress endPoint2;
        private bool bonus = false;
        private int msgCount = 0;
        private int pace = 0;
        private Object receiveLock = new Object();
        private Object messageLock = new Object();
        private LogBuilder lb = new LogBuilder();
        //private ClientConnection clientEndPoint1;
        //private ClientConnection clientEndPoint2;
        
        /// <summary>
        /// Non-Default constructor, takes 2 endPoints for
        /// the bonus portion of assignment
        /// </summary>
        /// <param name="wirelessNicIndex">
        /// index to set the wirelss nic to
        /// </param>
        /// <param name="endPoint1">
        /// server 1 endPoint ip Address
        /// </param>
        /// <param name="msgCount">
        /// number of messages to send/receive
        /// </param>
        /// <param name="pace">
        /// pause for the client pace
        /// </param>
        /// <param name="testDataTextbox">
        /// local output box to print debug/error messages
        /// </param>
        public HostConnection(int wirelessNicIndex, string endPoint1, 
            int msgCount, int pace, 
            System.Windows.Forms.RichTextBox testDataTextbox)
        {
            this.wirelessNicIndex = wirelessNicIndex;
            this.endPoint1 = IPAddress.Parse(endPoint1);
            this.msgCount = msgCount;
            this.pace = pace;
            this.testDataTextbox = testDataTextbox;
        }

        /// <summary>
        /// Non-Default constructor, takes 2 endPoints for
        /// the bonus portion of assignment
        /// </summary>
        /// <param name="wirelessNicIndex">
        /// index to set the wirelss nic to
        /// </param>
        /// <param name="endPoint1">
        /// server 1 endPoint ip Address
        /// </param>
        /// <param name="endPoint2">
        /// server 2 endPoint ip address
        /// </param>
        /// <param name="msgCount">
        /// number of messages to send/receive
        /// </param>
        /// <param name="pace">
        /// pause for the client pace
        /// </param>
        /// <param name="testDataTextbox">
        /// local output box to print debug/error messages
        /// </param>
        public HostConnection(int wirelessNicIndex, string endPoint1, 
            string endPoint2, int msgCount, int pace, 
            System.Windows.Forms.RichTextBox testDataTextbox)
        {
            this.wirelessNicIndex = wirelessNicIndex;
            this.endPoint1 = IPAddress.Parse(endPoint1);
            this.endPoint2 = IPAddress.Parse(endPoint2);
            this.msgCount = msgCount;
            this.pace = pace;
            bonus = true;
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
            IPEndPoint localEndPoint = new IPEndPoint(
                localIP.AddressList[wirelessNicIndex], PORT);
            localServerIP = IPAddress.Parse(localEndPoint.ToString().Split(':')[0]);

            // Set up host socket
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, 
                ProtocolType.Tcp);
            sock.Bind(localEndPoint);
            sock.Listen((int)SocketOptionName.MaxConnections);

            testDataTextbox.Text += localServerIP.ToString() + "\r\n";
        }

        /// <summary>
        /// Accepts connections from clients
        /// </summary>
        private void AcceptConnections()
        {
            Socket socket = null;

            // Accept connection from client
            try
            {
                socket = sock.Accept();
            }
            catch (Exception e)
            {
                testDataTextbox.Text += "ERROR! " + e.Message;
            }

            // Set up the server state saver
            ServerStateSaver serverState = new ServerStateSaver();
            serverState.serverSocket = socket;
            serverState.serverStopWatch.Start();

            // Set up the local client connection
            //clientEndPoint1 = new ClientConnection();
            //clientEndPoint1.ServerState = serverState;
            serverState.localClient = new ClientConnection();
            serverState.localClient.EndPoint = endPoint1;
            serverState.localClient.ServerState = serverState;
            serverState.localClient.testDataTextbox = testDataTextbox;

            // Connect to the endpoint server
            serverState.localClient.Start();

            // Set up and start thread on connection handler function
            serverState.serverThread = new Thread(delegate()
                {
                    ConnectionHandler(serverState);
                });

            serverState.serverThread.Start();
        }

        /// <summary>
        /// Handles the connection to the client
        /// </summary>
        /// <param name="serverState">
        /// ServerStateSaver object to access
        /// current properties dynamically.
        /// </param>
        private void ConnectionHandler(ServerStateSaver serverState)
        {
            // initialize buffer array
            byte[] buffer = new byte[BUFFER_SIZE];

            // message length buffer array
            byte[] byteSize = new byte[LENGTH_BITS];

            // bytes received last read
            int bytesRead = 0;

            // message counter
            int messageCount = 0;

            while (true)
            {
                // message array pointers
                int offSet = 0;
                int size = 0;

                lock (receiveLock)
                {
                    bytesRead = serverState.serverSocket.Receive(buffer, 
                        offSet, LENGTH_BITS, SocketFlags.None);
                }
                
                // Get the size values out of current message
                Array.Copy(buffer, offSet, byteSize, 0, LENGTH_BITS);

                // Reverse the bits if they aren't in proper order for proc
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(byteSize);
                }

                // Set the size variable
                size = BitConverter.ToInt16(byteSize, 0);

                // Set offSet variable
                offSet += LENGTH_BITS;

                lock (messageLock)
                {
                    // read next message out of buffer
                    bytesRead = serverState.serverSocket.Receive(buffer, offSet, size, 
                        SocketFlags.None);
                }

                // Set messageBuffer to correct size
                serverState.serverMessage = new byte[size];

                // Copy message into the messageBuffer
                Array.Copy(buffer, offSet, serverState.serverMessage, 0, size);

                // Send the message off to be processed
                Thread processMessageThread = new Thread(delegate()
                    {
                        ProcessMessage(serverState);
                    });
                serverState.serverThread.Start();

                // increment the message counter
                messageCount++;
            }
        }

        private void ProcessMessage(ServerStateSaver serverState)
        {
            // save message to log builder dictionary
            lb.hostReqMessage.Add(serverState.messageCount, serverState.serverMessage);

            // create instance of the ResponseBuilder
            ResponseBuilder rb = new ResponseBuilder(serverState);
            rb.HostResponse();

        }
    }
}
