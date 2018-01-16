using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client_Server_Practice_DataTemplate.Communication
{
    public class Server
    {
        private int port;
        private Action<string> updateGUI;

        Socket serverSocket;

        List<ClientHandler> ClientList = new List<ClientHandler>();

        public Server(int port, Action<string> updateGUI)
        {
            this.port = port;
            this.updateGUI = updateGUI;

            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Loopback, port));
            serverSocket.Listen(5);
            Task.Factory.StartNew(Accept);
        }

        private void Accept()
        {
            while (true)
            {
                try
                {
                    ClientList.Add(new ClientHandler(serverSocket.Accept(), ReceivedMessage));
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public void ReceivedMessage(string message)
        {
            foreach (var item in ClientList)
            {
                item.Send(message);
            }

            updateGUI(message);
        }

        public void Send(string message)
        {
            foreach (var client in ClientList)
            {
                client.Send(message);
            }
        }
    }
}
