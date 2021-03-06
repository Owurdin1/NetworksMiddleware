﻿using System;
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
        private const int LENGTH_BITS = 2;


        // global private variables
        private IPAddress endPoint;
        private Socket sock;
        private int msgCount = 0;
        private ServerStateSaver serverState;
        private List<byte[]> sendMessages = new List<byte[]>();
        private List<byte[]> recMessages = new List<byte[]>();
        private Object clientSendLock = new Object();

        // global public variables
        //public System.Windows.Forms.RichTextBox testDataTextbox;
        
        /// <summary>
        /// sets or gets the message count
        /// </summary>
        public int MsgCount
        {
            get { return msgCount; }
            set { msgCount = value; }
        }

        /// <summary>
        /// gets or sets the endPoint to connect to
        /// </summary>
        public IPAddress EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }

        public ClientConnection(ServerStateSaver serverState)
        {
            this.serverState = serverState;
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
            catch (Exception)
            {
                //testDataTextbox.Text += e.Message;
            }

            return sock;
        }

        // begins sending message on to the endPoint
        public void BeginTransmission()
        {
            // Create necessary threads to handle client connection
            serverState.clientState.clientThread = new Thread(delegate()
                {
                    ThreadSendFunction(serverState);
                });

            try
            {
                // Start threads listening and sending
                serverState.clientState.clientThread.Start();
            }
            catch (Exception)
            {
            }

            //receiveThread.Join();
            //serverState.clientState.clientThread.Join();
        }

        /// <summary>
        /// Handles the connection to the endpoint server
        /// sends messages from middlware to endpoint.
        /// </summary>
        /// <param name="clientState">
        /// CleintStateSaver object.
        /// </param>
        private void ThreadSendFunction(ServerStateSaver serverState)
        {
            try
            {
                // sleep the thread based on pace given in gui
                Thread.Sleep(serverState.clientState.pace);

                // lock socket and send the message to endpoint server
                lock (clientSendLock)
                {
                    serverState.clientState.clientSocket.Send(serverState.sendMsg);
                }

                // put the message into the log builder
                lock (serverState.serverMessageAdd)
                {
                    serverState.lb.messageLogList.Add(serverState.sendMsg);
                }
            }
            catch (Exception)
            {
            }
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
            int receivedBytes = 0;
            byte[] byteSize = new byte[LENGTH_BITS];
            byte[] clientBuffer = serverState.clientState.clientBuffer;

            try
            {
                do
                {
                    //int offset = 0;
                    //int size = 0;

                    //receivedBytes = serverState.clientState.clientSocket.Receive(clientBuffer,
                    //    offset, LENGTH_BITS, SocketFlags.None);

                    //Array.Copy(clientBuffer, offset, byteSize, 0, LENGTH_BITS);

                    //if (BitConverter.IsLittleEndian)
                    //{
                    //    Array.Reverse(byteSize);
                    //}

                    //size = BitConverter.ToInt16(byteSize, 0);

                    //offset += LENGTH_BITS;

                    //receivedBytes = serverState.clientState.clientSocket.Receive(clientBuffer,
                    //    offset, size, SocketFlags.None);

                    //serverState.returnMsg = new byte[size];

                    //Array.Copy(clientBuffer, offset, serverState.returnMsg, 0, size);

                    //ReturnMessageClass rmc = new ReturnMessageClass();

                    //Thread returnThread = new Thread(delegate()
                    //    {
                    //        rmc.ReturnMessage(serverState);
                    //    });
                    //returnThread.Start();

                    receivedBytes =
                        serverState.clientState.clientSocket.Receive
                        (serverState.clientState.clientBuffer);

                    serverState.returnMsg = new byte[receivedBytes];

                    Array.Copy(serverState.clientState.clientBuffer,
                        serverState.returnMsg, receivedBytes);

                    serverState.clientState.clientMsgCount++;

                    ReturnMessageClass rmc = new ReturnMessageClass();

                    Thread returnThread = new Thread(delegate()
                        {
                            rmc.ReturnMessage(serverState);
                        });

                    returnThread.Start();
                }
                while (receivedBytes > 0);
            }
            catch (Exception)
            { 

            }
        }
    }
}
