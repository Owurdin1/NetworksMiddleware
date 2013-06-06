using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareNetworks.Classes
{
    class LogBuilder
    {
        internal List<byte[]> incomingMessages = new List<byte[]>();
        internal List<byte[]> processedMessages = new List<byte[]>();
        internal List<byte[]> routedMessages = new List<byte[]>();
    }
}
