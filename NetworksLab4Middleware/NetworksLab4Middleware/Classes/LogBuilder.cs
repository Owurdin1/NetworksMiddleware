using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NetworksLab4Middleware.Classes
{
    class LogBuilder
    {
        // global constant variables
        private const string PATH = @"C:\Logs\Middleware\Log.txt";
        private const int LENGTH_BITS = 2;

        // global private variables


        // global public variables
        public List<byte[]> messageLogList = new List<byte[]>();
        //public Dictionary<int, byte[]> hostForwardMessage = new Dictionary<int, byte[]>();
        //public Dictionary<int, byte[]> clientResponseMessage = new Dictionary<int, byte[]>();

        /// <summary>
        /// Writes the log file 
        /// </summary>
        /// <param name="serverState"></param>
        public void WriteLogs(ServerStateSaver serverState)
        {
            StreamWriter sw = File.AppendText(PATH);

            foreach (byte[] item in messageLogList)
            {
                // Remove the size bits from message
                byte[] noSize = new byte[item.Length - LENGTH_BITS];
                Array.Copy(item, LENGTH_BITS, noSize, 0, noSize.Length);

                string msg = System.Text.Encoding.ASCII.GetString(noSize);
                sw.Write(msg);
            }

            sw.Close();
        }
    }
}
