using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace NetworksLab4Middleware.Classes
{
    class ClientStateSaver
    {
        public Socket clientSocket = null;
        public Thread clientThread = null;
        public List<byte[]> clientMessages = null;
        public Stopwatch clientStopWatch = null;
        
        public ClientStateSaver()
        {
            clientStopWatch = new Stopwatch();
        }
    }

    class ServerStateSaver
    {
        public Socket serverSocket = null;
        public Thread serverThread = null;
        public byte[] serverMessage = null;
        public Stopwatch serverStopWatch = null;
        public int messageCount = 0;
        public ClientConnection localClient = new ClientConnection();

        public ServerStateSaver()
        {
            serverStopWatch = new Stopwatch();
        }
    }
}
