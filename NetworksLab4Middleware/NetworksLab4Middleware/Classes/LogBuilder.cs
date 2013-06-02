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

        // global private variables


        // global public variables
        public Dictionary<int, byte[]> clientReqMessage = new Dictionary<int, byte[]>();
        public Dictionary<int, byte[]> hostForwardMessage = new Dictionary<int, byte[]>();
        public Dictionary<int, byte[]> clientResponseMessage = new Dictionary<int, byte[]>();

        public void WriteLogs(ServerStateSaver serverState)
        {

        }
    }
}
