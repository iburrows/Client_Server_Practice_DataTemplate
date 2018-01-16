using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Client_Server_Practice_DataTemplate.ViewModel
{
    public class MessageVM
    {
        public string Message { get; set; }
        public string MessageTime { get; set; }

        public MessageVM(string message, string messageTime)
        {
            Message = message;
            MessageTime = messageTime;            
        }


    }
}
