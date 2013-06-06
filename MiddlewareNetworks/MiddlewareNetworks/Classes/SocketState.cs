using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareNetworks.Classes
{
    class SocketState
    {
        public int incomingNumber = 0;  // Incoming message counter
        public int serverMsgCounter = 0; // endpoint message counter
        public System.Net.Sockets.Socket sock = null; // socket containing client side connection
        public System.Net.Sockets.Socket serverSock = null; // socket containing endpoint connection
        public System.Diagnostics.Stopwatch serverStpWatch = new System.Diagnostics.Stopwatch();
        public System.Diagnostics.Stopwatch stpWatch = new System.Diagnostics.Stopwatch();
        public System.Threading.Thread thread; // thread tied to connection handler function
        public System.Threading.Thread serverThread; // thread tied to endpoint connection
        public System.Net.IPAddress serverIPAddress1 = null; // endpoint server ip address
        public byte[] incomingBuffer = null;  // client incoming buffer
        public byte[] serverBuffer = null; // endpoint incoming buffer
        public Object receiveLock = new Object();  // receive lock on client socket
        public Object sendLock = new Object();      // send lock on client socket
        public Object serverReceiveLock = new Object(); // receive lock on server socket
        public Object serverSendLock = new Object(); // send lock on server socket
    }
}
