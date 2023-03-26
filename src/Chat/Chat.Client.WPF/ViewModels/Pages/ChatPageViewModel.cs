using Chat.Client.WPF.Commands;
using Chat.Client.WPF.Controls;
using Chat.Client.WPF.Entities;
using Chat.Client.WPF.Extensions;
using Chat.Client.WPF.Models;
using Chat.Client.WPF.ViewModels.UserControls;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Chat.Client.WPF.ViewModels.Pages
{
    internal class ChatPageViewModel : ViewModelBase
    {
        //public ChatListViewModel ChatList { get; set; }
        public ObservableCollection<ChatCardViewModel> Chats { get; set; }

        private ChatCardViewModel _selectedChat;
        public ChatCardViewModel SelectedChat
        {
            get => _selectedChat;
            set
            {
                _selectedChat = value;

                OnPropertyChanged(nameof(SelectedChat));
                OnPropertyChanged(nameof(SelectedChat.Messages));
            }
        }

        private ICommand _initializeViewModelCommand;
        public ICommand InitializeViewModelCommand => _initializeViewModelCommand;

        public ChatPageViewModel()
        {
            Chats = new ObservableCollection<ChatCardViewModel>()
            {

            };

            SelectedChat = default(ChatCardViewModel);

            _initializeViewModelCommand = new RelayCommand((o) => OnLoad());
        }


        private async void OnLoad()
        {
            Chats.Clear();

            await Task.Delay(50);
            OnNewChat(new ChatEntity()
            {
                ContactUsername = "Adam"
            });
            await Task.Delay(50);

            OnNewChat(new ChatEntity()
            {
                ContactUsername = "Admin"
            });

            OnNewChat(new ChatEntity()
            {
                ContactUsername = "Pussy"
            });
            OnNewChat(new ChatEntity()
            {
                ContactUsername = "AАПdmin3"
            });
            OnNewChat(new ChatEntity()
            {
                ContactUsername = "АВФЫAРЫФРЫdmin4"
            });
            OnNewChat(new ChatEntity()
            {
                ContactUsername = "AdmПЫФФРЫПРЫФin5"
            });

        }

        private void OnNewChat(ChatEntity chat)
        {
            GetColors(chat.ContactUsername, out var avatarfillColor, out var avatarForeColor);

            var avatarViewModel = new AvatarViewModel
            (
                avatarLabel: chat.ContactUsername.First().ToString(),
                hash: chat.ContactUsername.GetStableHashCode()
            );

            var chatViewModel = new ChatCardViewModel(chat.ContactUsername, avatarViewModel);
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            chatViewModel.Messages.Add(new MessageViewModel()
            {
                Id = 134,
                IsFromMe = random.Next(0, 2) % 2 == 0,
                OutputText = "fdsagggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg",
                Time = DateTime.Now.ToShortTimeString()
            });

            Chats.Add(chatViewModel);
        }

        private static void GetColors(string username, out SolidColorBrush fillColor, out SolidColorBrush foreColor)
        {
            int hash = username.GetStableHashCode(); // Convert.ToInt32(text); // string.GetHashCode(text);

            if (hash < 0)
                hash *= -1;

            int index = hash % _colors.Length;

            fillColor = new SolidColorBrush(_colors[index].ForeColor);
            foreColor = new SolidColorBrush(_colors[index].BackColor);
        }

        private static AvatarColors[] _colors = new AvatarColors[]
        {
            new AvatarColors(Color.FromRgb(248, 230, 242), Color.FromRgb(195, 120, 164)),
            new AvatarColors(Color.FromRgb(227,229,254), Color.FromRgb(140,135,223)),
            new AvatarColors(Color.FromRgb(219,243,252), Color.FromRgb(116,188,218)),
            new AvatarColors(Color.FromRgb(249,231,215), Color.FromRgb(230, 159, 92)),
        };
    }
}
