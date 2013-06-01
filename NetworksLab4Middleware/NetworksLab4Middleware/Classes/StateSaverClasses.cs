using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace NetworksLab4Middleware.Classes
{
    class ClientStateSaver
    {
        public Socket clientSocket = null;
        public Thread clientThread = null;
        public List<string> clientMessages = new List<string>();
        public Stopwatch clientStopWatch = new Stopwatch();
    }

    class ServerStateSaver
    {
        public Socket serverSocket = null;      /* socket attatched to this object */
        public Thread serverThread = null;      /* Thread attatched to this object */
        public byte[] serverMessage = null;     /* message from client */
        public byte[] sendMsg = null;           /* message send to endpoint server */
        public byte[] returnMsg = null;         /* message to be passed back to client */
        public Stopwatch serverStopWatch = new Stopwatch();
        public int messageCount = 0;            /* counter for the number of messages */
        public ClientConnection localClient = new ClientConnection();   /* clientconnection object */
        public IPAddress serverEndPointIP = null;   /* endpoint for server messages to be sent to */
        public IPAddress localServerIP = null;  /* local server ip address */
        public ClientStateSaver clientState;
    }
}
