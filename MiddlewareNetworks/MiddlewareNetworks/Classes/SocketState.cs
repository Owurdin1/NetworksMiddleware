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
        public System.Net.Sockets.Socket sock = null; // socket containing client side connection
        public System.Diagnostics.Stopwatch stpWatch = new System.Diagnostics.Stopwatch();
        public System.Threading.Thread thread; // thread tied to connection handler function
        //public byte[] incomingBuffer = null;  // client incoming buffer
        public Object receiveLock = new Object();  // receive lock on client socket
        public ClientState clientState = new ClientState();
    }

    class ClientState
    {
        public int serverMsgCounter = 0; // endpoint message counter
        public int serverSentCounter = 0; // sent message endpoint counter
        public int messageCount = 0; // number of messages that the middleware is forwarding
        public int pace = 0; // pace for sending messages
        public System.Net.Sockets.Socket serverSock = null; // socket containing endpoint connection
        public System.Net.Sockets.Socket clientSock = null; // socket containing client connection
        public System.Net.IPAddress serverIPAddress1 = null; // endpoint server ip address
        public System.Net.IPAddress serverIPAddress2 = null; // endpoint server 2 IP address
        //public byte[] serverBuffer = null; // endpoint incoming buffer
        //public byte[] processedMessage = null; // message being sent to endpoint
        //public byte[] forwardMessage = null; // Message being forwarded to the client from endpoint
        public System.Threading.Thread serverThread = null; // thread tied to endpoint connection
        public LogBuilder lb = new LogBuilder();
        public Object serverReceiveLock = new Object(); // receive lock on server socket
        public Object serverSendLock = new Object(); // send lock on server socket
        public Object clientSendLock = new Object();      // send lock on client socket
    }
}
