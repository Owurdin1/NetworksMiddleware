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
            
            string previousMsg = string.Empty;

            foreach (byte[] item in messageLogList)
            {
                string msg = System.Text.Encoding.ASCII.GetString(item) + "\n\r";

                if (!previousMsg.Contains(msg))
                {
                    sw.Write(msg);
                    previousMsg += msg;
                }
            }

            sw.Close();
        }
    }
}
