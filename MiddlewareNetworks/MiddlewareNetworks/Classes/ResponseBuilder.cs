using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareNetworks.Classes
{
    class ResponseBuilder
    {
        /// <summary>
        /// Builds response message and prepares byte array
        /// </summary>
        /// <param name="count">
        /// integer value that is a unique value to append to messages
        /// </param>
        /// <returns>
        /// formatted byte array
        /// </returns>
        public byte[] Response(SocketState sockState, byte[] byteMessage)
        {
            byte[] msgByte = null;

            //string msg = System.Text.Encoding.ASCII.GetString(sockState.incomingBuffer);
            string msg = System.Text.Encoding.ASCII.GetString(byteMessage);
            string[] msgArray = msg.Split('|');

            msgArray[0] = "RSP";
            msgArray[1] = sockState.stpWatch.ElapsedMilliseconds.ToString();
            msgArray[6] = sockState.sock.RemoteEndPoint.AddressFamily.ToString();
            msgArray[7] = "2605";
            msgArray[8] = sockState.sock.Handle.ToString();
            msgArray[9] = sockState.clientState.serverIPAddress1.ToString();
            msgArray[11] = "OWMid " + sockState.incomingNumber.ToString();

            string message = String.Empty;

            message = string.Join("|", msgArray);
            message.ToString();

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
