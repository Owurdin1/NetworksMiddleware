using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworksLab4Middleware.Classes
{
    internal class ResponseBuilder
    {
        // Global private variables
        private ServerStateSaver serverState;

        /// <summary>
        /// Non-default constructor, takes a ServerStateSaver
        /// object so response can be created to be passed to
        /// endpoint server
        /// </summary>
        /// <param name="serverState">
        /// ServerStateSaver object with message
        /// </param>
        public ResponseBuilder(ServerStateSaver serverState)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public void HostResponse()
        {
            throw new NotImplementedException();
        }
    }
}
