using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace MiddlewareNetworks.Classes
{
    class ServerRun
    {
        // Global constant values
        private const int PORT = 2605;
        private const int BUFFER_SIZE = 256;
        private const int LENGTH_BITS = 2;

        // Private global variables
        private int wirelessNicIndex;
        private string endPoint1;
        private int msgCount;
        private int pace;
        private string endPoint2;
        private Socket sock = null;

        /// <summary>
        /// Non-Default constructor, this is called for only 
        /// one endpoint server.
        /// </summary>
        /// <param name="p1">
        /// integer for the wireless nic index
        /// </param>
        /// <param name="p2">
        /// string, endpoint server ip address
        /// </param>
        /// <param name="p3">
        /// int number of messages
        /// </param>
        /// <param name="p4">
        /// int pace for sending to endpoint
        /// </param>
        public ServerRun(int p1, string p2, int p3, int p4)
        {
            this.wirelessNicIndex = p1;
            this.endPoint1 = p2;
            this.msgCount = p3;
            this.pace = p4;
        }

        /// <summary>
        /// Non-Default constructor, this is called for only 
        /// one endpoint server.
        /// </summary>
        /// <param name="p1">
        /// integer for the wireless nic index
        /// </param>
        /// <param name="p2">
        /// string, endpoint server ip address
        /// </param>
        /// <param name="p5">
        /// string, endpoint second server ip address
        /// </param>
        /// <param name="p3">
        /// int number of messages
        /// </param>
        /// <param name="p4">
        /// int pace for sending to endpoint
        /// </param>
        public ServerRun(int p1, string p2, string p5, int p3, int p4)
        {
            this.wirelessNicIndex = p1;
            this.endPoint1 = p2;
            this.endPoint2 = p5;
            this.msgCount = p3;
            this.pace = p4;
        }

        /// <summary>
        /// Starts setting up the socket and calls
        /// threads to accept and process connections
        /// </summary>
        internal void Start()
        {
            SetSock();
            Thread acceptThread = new Thread(AcceptConnections);
            acceptThread.IsBackground = true;
            acceptThread.Start();
        }

        /// <summary>
        /// Accepts connections and spawns thread to handle them
        /// </summary>
        private void AcceptConnections()
        {
            Socket socket = null;

            while (true)
            {
                // Accepts a connection on socket
                try
                {
                    socket = sock.Accept();
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                }

                SocketState sockState = new SocketState();
                sockState.serverIPAddress1 = IPAddress.Parse(endPoint1);
                sockState.sock = socket;
                sockState.stpWatch.Start();

                // Spawns thread and starts ConnectionHandler function
                sockState.thread = new Thread(delegate()
                {
                    ConnectionHandler(sockState);
                });
                sockState.thread.IsBackground = true;
                sockState.thread.Start();
            }
        }

        /// <summary>
        /// Handles the connection that is attached to the
        /// SocketState object
        /// </summary>
        /// <param name="sockState">
        /// SocketState object to saving state values
        /// </param>
        private void ConnectionHandler(SocketState sockState)
        {
            // Incoming buffer byte array
            byte[] buffer = new byte[BUFFER_SIZE];

            // Message length byte array, stripped out of buffer
            byte[] byteSize = new byte[LENGTH_BITS];

            // bytes received last read
            int bytesRead = 0;

            int messageCount = 0;

            try
            {
                while (true)
                {
                    // Current message receive
                    int offSet = 0;
                    int size = 0;

                    lock (sockState.receiveLock)
                    {
                        bytesRead = sockState.sock.Receive(buffer, offSet, LENGTH_BITS, SocketFlags.None);
                    }
                    if (bytesRead == 0)
                    {
                        return;
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

                    lock (sockState.receiveLock)
                    {
                        // Read next message out of buffer
                        bytesRead = sockState.sock.Receive(buffer, offSet, size, SocketFlags.None);
                    }

                    // Set messageBuffer to new byte[] with size index
                    sockState.incomingBuffer = new byte[size];

                    // Copy message to messageBuffer
                    Array.Copy(buffer, offSet, sockState.incomingBuffer, 0, size);

                    // Send message off, exit do while
                    Thread senderThread = new Thread(delegate()
                    {
                        //SendFunction(messageBuffer, sockState);
                        SendFunction(sockState);
                    });
                    senderThread.Start();

                    // Increment the message count
                    messageCount++;
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }

            sockState.sock.Close();
        }

        /// <summary>
        /// Processes the sending funcitons of the server.
        /// This function will process messages then send
        /// them to alternate endpoint server.
        /// </summary>
        /// <param name="sockState">
        /// SocketState state saver object
        /// </param>
        private void SendFunction(SocketState sockState)
        {
            byte[] processedBuffer;

            // Process the incoming message and prepares it for response
            sockState.incomingNumber++;
            try
            {
                ResponseBuilder rb = new ResponseBuilder();
                processedBuffer = rb.Response(sockState);

                lock (sockState.sendLock)
                {
                    // Send message back to the client
                    sockState.sock.Send(processedBuffer);
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        /// <summary>
        /// Sets up the socket object before allowing it to
        /// be assigned to the SocketState object
        /// </summary>
        private void SetSock()
        {
            // Get local information
            IPHostEntry localIP = Dns.GetHostEntry(Dns.GetHostName());
            IPEndPoint localEndPoint = new IPEndPoint(
                localIP.AddressList[wirelessNicIndex], PORT);
            IPAddress localServerIP = IPAddress.Parse(localEndPoint.ToString().Split(':')[0]);

            // Set up host socket
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                ProtocolType.Tcp);
            sock.Bind(localEndPoint);
            sock.Listen((int)SocketOptionName.MaxConnections);
        }
    }
}
