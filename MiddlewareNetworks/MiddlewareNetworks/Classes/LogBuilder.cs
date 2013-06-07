using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MiddlewareNetworks.Classes
{
    class LogBuilder
    {
        private const string PATH = @"C:\Logs\Middleware\Log.txt";

        internal List<byte[]> incomingMessages = new List<byte[]>();
        internal List<byte[]> processedMessages = new List<byte[]>();
        internal List<byte[]> routedMessages = new List<byte[]>();

        public void LogWrite()
        {
            StreamWriter sw = new StreamWriter(PATH, true);
            string incMsg = string.Empty;
            string procMsg = string.Empty;
            string routMsg = string.Empty;

            for (int i = 0; i < incomingMessages.Count; i++)
            {
                incMsg = System.Text.Encoding.ASCII.GetString(incomingMessages[i]);
                sw.Write(incMsg + "\r\n");

                if (i < processedMessages.Count)
                {
                    procMsg = System.Text.ASCIIEncoding.ASCII.GetString(processedMessages[i]);
                    sw.Write(procMsg + "\r\n");

                    if (i < routedMessages.Count)
                    {
                        routMsg = System.Text.ASCIIEncoding.ASCII.GetString(routedMessages[i]);
                        sw.Write(routMsg + "\r\n");
                    }
                }
            }
        }
    }
}
