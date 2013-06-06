using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MiddlewareNetworks.Classes
{
    class ClientRun
    {
        // private constant values
        private const int PORT = 2605;
        private const int LENGTH_BITS = 2;
        private const int BUFFER_SIZE = 256;

        // private global variables
        private System.Net.IPAddress endPointIP = null;
        private Object forwardMessageLock = new Object();

        /// <summary>
        /// sets up the clientstate object to prepare
        /// for send/receive operations from endpoint
        /// connection
        /// </summary>
        /// <param name="sockState">
        /// takes a socketState object
        /// </param>
        /// <returns>
        /// socket connected to endpoint server
        /// </returns>
        internal System.Net.Sockets.Socket SetClientSocket(ClientState clientState)
        {
            endPointIP = clientState.serverIPAddress1;
            Connect(clientState);

            return clientState.serverSock;
        }

        /// <summary>
        /// Connects to the endpoint
        /// </summary>
        /// <param name="clientState">
        /// takes a clientstate object and connects it to endpoint
        /// </param>
        private void Connect(ClientState clientState)
        {
            System.Net.Sockets.Socket sock = new System.Net.Sockets.Socket(
                System.Net.Sockets.AddressFamily.InterNetwork,
               System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);

            try
            {
                sock.Connect(endPointIP, PORT);

                sock.SetSocketOption(System.Net.Sockets.SocketOptionLevel.Tcp,
                    System.Net.Sockets.SocketOptionName.NoDelay, true);

                clientState.serverSock = sock;
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }

        /// <summary>
        /// Receive thread to endpoint.
        /// Should spin until all messages received
        /// </summary>
        /// <param name="clientState">
        /// clientstate object</param>
        internal void ClientReceive(ClientState clientState)
        {
            //try
            //{
                int receivedMessages = 0;
                byte[] byteSize = new byte[LENGTH_BITS];
                //clientState.serverBuffer = new byte[BUFFER_SIZE];
                byte[] serverBuffer = new byte[BUFFER_SIZE];
                //byte[] forwardMessage;

                while (receivedMessages < clientState.messageCount)
                {
                    int offset = 0;
                    int size = 0;
                    int bytesRead = 0;
                    //clientState.serverSock.ReceiveTimeout = 2000;

                    //bytesRead = clientState.serverSock.Receive(clientState.serverBuffer, offset,
                    //    LENGTH_BITS, System.Net.Sockets.SocketFlags.None);
                    bytesRead = clientState.serverSock.Receive(serverBuffer, offset,
                        LENGTH_BITS, System.Net.Sockets.SocketFlags.None);

                    //Array.Copy(clientState.serverBuffer, offset, byteSize, 0, LENGTH_BITS);
                    Array.Copy(serverBuffer, offset, byteSize, 0, LENGTH_BITS);

                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(byteSize);
                    }

                    size = BitConverter.ToInt16(byteSize, 0);
                    offset += LENGTH_BITS;

                    //bytesRead = clientState.serverSock.Receive(clientState.serverBuffer, offset,
                    //    size, System.Net.Sockets.SocketFlags.None);
                    bytesRead = clientState.serverSock.Receive(serverBuffer, offset,
                        size, System.Net.Sockets.SocketFlags.None);

                    //clientState.forwardMessage = new byte[bytesRead];
                    //clientState.forwardMessage = new byte[bytesRead];
                    //Array.Copy(clientState.serverBuffer, offset, clientState.forwardMessage, 0, bytesRead);
                    byte[] forwardMessage = new byte[bytesRead];
                    //Array.Copy(serverBuffer, offset, forwardMessage, 0, bytesRead);
                    Array.Copy(serverBuffer, 0, forwardMessage, 0, bytesRead);

                    //clientState.lb.routedMessages.Add(clientState.forwardMessage);
                    clientState.lb.routedMessages.Add(forwardMessage);

                    receivedMessages++;

                    Thread routeMsgThread = new Thread(delegate()
                    {
                        RouteMessage(clientState, forwardMessage);
                    });
                    routeMsgThread.Start();
                }
            //}
            //catch (Exception e)
            //{
            //    e.Message.ToString();
            //}

            //routeMsgThread.Join();

            //System.Windows.Forms.MessageBox.Show("Received All Messages");
        }

        /// <summary>
        /// forwards message from endpoint server to client
        /// </summary>
        /// <param name="clientState">
        /// ClientState object
        /// </param>
        private void RouteMessage(ClientState clientState, byte[] forwardMessage)
        {
            try
            {
                lock (clientState.clientSendLock)
                {
                    //clientState.clientSock.Send(clientState.forwardMessage);
                    clientState.clientSock.Send(forwardMessage);
                }
            }
            catch (Exception e)
            {
                string byteME = System.Text.Encoding.ASCII.GetString(forwardMessage);
                e.Message.ToString();
            }
        }

        /// <summary>
        /// sends message to endpoint
        /// </summary>
        /// <param name="clientState">
        /// ClientState object
        /// </param>
        internal void ClientSend(ClientState clientState, byte[] sendMsg)
        {
            try
            {
                // Send clientState.Processed message to server
                Thread.Sleep(clientState.pace);

                // lock and send to the server
                lock (clientState.serverSendLock)
                {
                    //clientState.serverSock.Send(clientState.processedMessage);
                    clientState.serverSock.Send(sendMsg);
                }

                clientState.serverSentCounter++;
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }
    }
}
