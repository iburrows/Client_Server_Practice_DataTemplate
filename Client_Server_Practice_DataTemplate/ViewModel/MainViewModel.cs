using Client_Server_Practice_DataTemplate.Communication;
using Client_Server_Practice_DataTemplate.TheClient;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;

namespace Client_Server_Practice_DataTemplate.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        public RelayCommand StartServer { get; set; }
        public RelayCommand StartClient { get; set; }
        public RelayCommand SendBtn { get; set; }

        public string Message
        {
            get => message;
            set { message = value; }
        }
        //public string MessageTime { get; set; }

        public ObservableCollection<MessageVM> MesssageList { get; set; }

        Server server;
        Client client;

        bool isClient = false;
        bool isServer = false;
        private string message = "";
        private DateTime _messageTime = DateTime.Now;
        private const int port = 10100;

        public DateTime MessageTime { get => _messageTime; set => _messageTime = value; }

        public MainViewModel()
        {
            MesssageList = new ObservableCollection<MessageVM>();

            StartServer = new RelayCommand(() =>
                {
                    server = new Server(port, UpdateGUI);
                    isServer = true;
                });
            StartClient = new RelayCommand(() =>
            {
                client = new Client(port, UpdateGUI);
                isClient = true;
            });

            SendBtn = new RelayCommand(
                () =>
                {
                    if (isClient)
                    {
                        client.Send(Message);
                    }
                    else
                    {
                        server.Send(Message);
                    }
                });
        }

        public void UpdateGUI(string message)
        {
            App.Current.Dispatcher.Invoke(
                () =>
                {
                    MesssageList.Add(new MessageVM(message, MessageTime.ToShortTimeString()));
                }
                );
        }
    }
}