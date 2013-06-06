using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworksLab4Middleware.Classes
{
    internal class ResponseBuilder
    {
        // Global private Constant variables
        private const int RESPONSE = 0;
        private const int MS_TIME = 1;
        private const int FORIEGN_HOST_IP = 6;
        private const int PORT_INDEX = 7;
        private const int SERVER_SOCK_NUMBER = 8;
        private const int SERVER_IP_ADDRESS = 9;
        private const int CUSTOM_FIELD = 11;

        // Global private variables
        private ServerStateSaver serverState;

        /// <summary>
        /// Non-default constructor, takes a ServerStateSaver
        /// object so response can be created to be passed to
        /// endpoint server
        /// </summary>
        /// <param name="serverState">
        /// ServerStateSaver object with message
        /// </param>
        public ResponseBuilder(ServerStateSaver serverState)
        {
            this.serverState = serverState;
        }

        /// <summary>
        /// Builds the response message from the server
        /// to send to the endpoint server.
        /// </summary>
        public void HostResponse()
        {
            // Get a string and string array out of the message
            string msg = System.Text.Encoding.ASCII.GetString(serverState.serverMessage);
            string[] msgArray = msg.Split('|');

            msgArray[RESPONSE] = "RSP";
            msgArray[MS_TIME] = serverState.serverStopWatch.ElapsedMilliseconds.ToString();
            msgArray[FORIEGN_HOST_IP] = serverState.serverSocket.RemoteEndPoint.AddressFamily.ToString();
            msgArray[PORT_INDEX] = "2605";
            msgArray[SERVER_IP_ADDRESS] = serverState.localServerIP.ToString();
            msgArray[CUSTOM_FIELD] = "OWMidForm " + serverState.messageCount.ToString();

            string message = string.Join("|", msgArray);

            SetMessageLength(System.Text.Encoding.ASCII.GetBytes(message));
        }

        private void SetMessageLength(byte[] msgByte)
        {
            short msgLength = (short)msgByte.Length;
            byte[] bitSize = BitConverter.GetBytes(msgLength);
            serverState.sendMsg = new byte[msgLength + bitSize.Length];

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bitSize);
            }

            Array.Copy(bitSize, serverState.sendMsg, bitSize.Length);
            Array.Copy(msgByte, 0, serverState.sendMsg, bitSize.Length, msgLength);
        }
    }
}
