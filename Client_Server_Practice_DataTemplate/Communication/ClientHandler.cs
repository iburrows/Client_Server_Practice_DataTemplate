using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client_Server_Practice_DataTemplate.Communication
{
    class ClientHandler
    {
        private Socket socket;
        private Action<string> receivedMessage;

        byte[] buffer = new byte[512];

        string endMessage = "quit";

        public ClientHandler(Socket socket, Action<string> receivedMessage)
        {
            this.socket = socket;
            this.receivedMessage = receivedMessage;

            Task.Factory.StartNew(Receive);
        }

        private void Receive()
        {
            string message = "";

            while (!message.Equals(endMessage))
            {
                int length = socket.Receive(buffer);
                message = Encoding.UTF8.GetString(buffer, 0, length);
                receivedMessage(message);
            }

            Close();
        }

        private void Close()
        {
            socket.Close(1);
        }

        public void Send(string message)
        {
            socket.Send(Encoding.UTF8.GetBytes(message));
        }
    }
}
