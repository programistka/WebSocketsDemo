using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocket4Net;

namespace WebSocketsClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            client.Setup("ws://127.0.0.1:2012", "basic", WebSocketVersion.Rfc6455);
            client.Start();
        }
    }
}
