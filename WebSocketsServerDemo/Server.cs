using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperWebSocket;
using SuperSocket.SocketBase;

namespace WebSocketsServerDemo
{
    class Server
    {
        private WebSocketServer appServer;

        public void Setup()
        {
            Console.WriteLine("Press any key to start the WebSocketServer!");
            Console.ReadKey();
            Console.WriteLine();

            appServer = new WebSocketServer();

            if (!appServer.Setup(2012)) //Setup with listening port
            {
                Console.WriteLine("Failed to setup!");
                Console.ReadKey();
                return;
            }

            appServer.NewSessionConnected   += new SessionHandler<WebSocketSession>(appServer_NewSessionConnected);
            appServer.SessionClosed         += new SessionHandler<WebSocketSession, CloseReason>(appServer_SessionClosed);
            appServer.NewMessageReceived    += new SessionHandler<WebSocketSession, string>(appServer_NewMessageReceived);

            Console.WriteLine();
        }
        
        public void Start()
        {
            if (!appServer.Start())
            {
                Console.WriteLine("Failed to start!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("The server started successfully! Press any key to see application options.");
            Console.ReadKey();

            ShowAvailableOptions(); 

            char keyStroked;

            while (true)
            {
                keyStroked = Console.ReadKey().KeyChar;

                if (keyStroked.Equals('q'))
                {
                    Stop();
                    return;
                }

                if (keyStroked.Equals('s'))
                {
                    Console.WriteLine();
                    Console.WriteLine("Put here your message to clients: ");

                    string message = Console.ReadLine();

                    foreach (WebSocketSession session in appServer.GetAllSessions())
                    {
                        session.Send("Message to client: " + message);
                    }
                }
                
                ShowAvailableOptions();
                continue;
            }
        }

        public void Stop()
        {
            appServer.Stop();

            Console.WriteLine();
            Console.WriteLine("The server was stopped!");
        }

        public void ShowAvailableOptions()
        {
            Console.WriteLine();
            Console.WriteLine("Available options: ");
            Console.WriteLine("Press 'q' key to stop the server.");
            Console.WriteLine("Press 's' key to send message to client.");
        }

        private void appServer_NewMessageReceived(WebSocketSession session, string message)
        {
            Console.WriteLine("Client said: " + message);
            //Send the received message back
            session.Send("Server responded back: " + message);
        }

        private void appServer_NewSessionConnected(WebSocketSession session)
        {
            Console.WriteLine();
            Console.WriteLine("New session connected! Sessions counter: " + appServer.SessionCount);
            session.Send("Hello new client!");
        }

        private void appServer_SessionClosed(WebSocketSession session, CloseReason value)
        {
            Console.WriteLine();
            Console.WriteLine("Client disconnected! Sessions counter: " + appServer.SessionCount);
        }
    }
}