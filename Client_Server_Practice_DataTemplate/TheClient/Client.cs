using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client_Server_Practice_DataTemplate.TheClient
{
    public class Client
    {
        private int port;
        private Action<string> updateGUI;
        byte[] buffer = new byte[512];
        Socket clientSocket;
        string endMessage = "quit";

        public Client(int port, Action<string> updateGUI)
        {
            this.port = port;
            this.updateGUI = updateGUI;

            TcpClient client = new TcpClient();
            client.Connect(new IPEndPoint(IPAddress.Loopback, port));

            clientSocket = client.Client;

            Task.Factory.StartNew(Receive);
        }

        private void Receive()
        {
            string message = "";

            while (!message.Equals(endMessage))
            {
                int length = clientSocket.Receive(buffer);
                message = Encoding.UTF8.GetString(buffer, 0, length);
                updateGUI(message);
            }

            Close();
        }

        public void Send(string message)
        {
            byte[] messageInBytes = Encoding.UTF8.GetBytes(message);
            clientSocket.Send(Encoding.UTF8.GetBytes(message));
        }

        private void Close()
        {
            clientSocket.Close(1);
        }
    }
}
