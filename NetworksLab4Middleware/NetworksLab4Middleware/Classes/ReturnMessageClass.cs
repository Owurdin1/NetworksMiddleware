using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworksLab4Middleware.Classes
{
    class ReturnMessageClass
    {
        // private global variables
        private Object sendLock = new Object();

        /// <summary>
        /// Returns the endpoint message back to the client
        /// </summary>
        /// <param name="serverState">
        /// server state saver class object
        /// </param>
        public void ReturnMessage(ServerStateSaver serverState)
        {
            // Add message to the log
            lock (serverState.serverMessageAdd)
            {
                serverState.lb.messageLogList.Add(serverState.returnMsg);
            }

            lock (sendLock)
            {
                serverState.serverSocket.Send(serverState.returnMsg);
            }
        }
    }
}
