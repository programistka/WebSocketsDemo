using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperWebSocket;
using SuperSocket.SocketBase;

namespace WebSocketsServerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Server myServer = new Server();

            myServer.Setup();
            myServer.Start();
        }
    }
}
