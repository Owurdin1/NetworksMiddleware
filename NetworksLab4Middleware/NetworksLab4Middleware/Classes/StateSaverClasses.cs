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
        public Socket serverSocket = null;
        public Thread serverThread = null;
        public byte[] serverMessage = null;
        public byte[] sendMsg = null;
        public byte[] returnMsg = null;
        public List<string> messageList = new List<string>();
        public Stopwatch serverStopWatch = new Stopwatch();
        public int messageCount = 0;
        public ClientConnection localClient = new ClientConnection();
        public IPAddress serverEndPointIP;
    }
}
