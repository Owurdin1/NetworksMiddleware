using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworksLab4Middleware.Classes
{
    class LogBuilder
    {
        // global private variables
        

        // public global variables
        public Dictionary<int, byte[]> clientReqMessage = new Dictionary<int, byte[]>();
        public Dictionary<int, byte[]> hostResponseMessage = new Dictionary<int, byte[]>();
        //public Dictionary<int, byte[]> clientReqMessage = new Dictionary<int, byte[]>();
        public Dictionary<int, byte[]> clientResponseMessage = new Dictionary<int, byte[]>();
    }
}
