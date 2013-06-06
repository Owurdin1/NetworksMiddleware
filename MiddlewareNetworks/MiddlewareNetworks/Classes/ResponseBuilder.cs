using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareNetworks.Classes
{
    class ResponseBuilder
    {
        // class private variables
        //private string msg = String.Empty;

        /// <summary>
        /// non default constructor, 
        /// only use this one to implement
        /// ResponseBuilder class
        /// </summary>
        /// <param name="msg">
        /// message form the client
        /// </param>
        //public ResponseBuilder(SocketState sockState)
        //{
        //    string msg = System.Text.Encoding.ASCII.GetString(sockState.incomingBuffer);
        //    string[] msgArray = msg.Split('|');

        //    if (msgArray.Length < 14)
        //    {
        //        //System.Windows.Forms.MessageBox.Show("ERROR: Message sent from client is invalid");
        //    }
        //}

        /// <summary>
        /// Builds response message and prepares byte array
        /// </summary>
        /// <param name="count">
        /// integer value that is a unique value to append to messages
        /// </param>
        /// <returns>
        /// formatted byte array
        /// </returns>
        public byte[] Response(SocketState sockState)
            //int count, string msTimeStamp,
            //string foreignHostIP, string serverSocketNumber, string serverIPAddress)
        {
            //    System.Text.Encoding.ASCII.GetString(messageBuffer));
            //processedBuffer = rb.Response(sockState.incomingNumber,
            //    sockState.stpWatch.ElapsedMilliseconds.ToString(),
            //    sockState.sock.RemoteEndPoint.AddressFamily.ToString(),
            //    sockState.sock.Handle.ToString(),
            //    localServerIP);
            byte[] msgByte = null;

            string msg = System.Text.Encoding.ASCII.GetString(sockState.incomingBuffer);
            string[] msgArray = msg.Split('|');

            msgArray[0] = "RSP";
            msgArray[1] = sockState.stpWatch.ElapsedMilliseconds.ToString(); //msTimeStamp;
            msgArray[6] = sockState.sock.RemoteEndPoint.AddressFamily.ToString(); // foreignHostIP;
            msgArray[7] = "2605";
            msgArray[8] = sockState.sock.Handle.ToString(); // serverSocketNumber;
            msgArray[9] = sockState.serverIPAddress1.ToString();
            msgArray[11] = "OW " + sockState.incomingNumber.ToString(); // count.ToString();

            string message = String.Empty;

            message = string.Join("|", msgArray);

            msgByte = System.Text.Encoding.ASCII.GetBytes(message);

            return SetMsgLength(msgByte);
        }

        /// <summary>
        /// Gets the length of the message and tack that value
        /// in bits onto the front of the byte array
        /// </summary>
        /// <param name="msgByte">
        /// byte array that will be sent back to calling function
        /// </param>
        /// <returns></returns>
        private byte[] SetMsgLength(byte[] msgByte)
        {
            // get length of message
            short msgLength = (short)msgByte.Length;

            // create a byte array fro the size of message
            byte[] bitSize = BitConverter.GetBytes(msgLength);

            // create a byte array so that size of message and message will fit
            byte[] sendMsg = new byte[msgLength + bitSize.Length];

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bitSize);
            }

            // Copy both arrays into new message byte array
            Array.Copy(bitSize, sendMsg, bitSize.Length);
            Array.Copy(msgByte, 0, sendMsg, bitSize.Length, msgLength);

            // return the message array back to calling function
            return sendMsg;
        }
    }
}
