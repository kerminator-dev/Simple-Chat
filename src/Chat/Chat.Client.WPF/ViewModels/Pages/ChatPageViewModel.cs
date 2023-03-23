using Chat.Client.WPF.Entities;
using Chat.Client.WPF.Extensions;
using Chat.Client.WPF.Models;
using Chat.Client.WPF.ViewModels.UserControls;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Chat.Client.WPF.ViewModels.Pages
{
    internal class ChatPageViewModel : ViewModelBase
    {
        //public ChatListViewModel ChatList { get; set; }
        public ObservableCollection<ChatCardViewModel> Chats { get; set; }

        public ChatPageViewModel()
        {
            Chats = new ObservableCollection<ChatCardViewModel>()
            {

            };

            OnNewChat(
             new ChatEntity()
             {
                 ContactUsername = "Pussy"
             });
         
             OnNewChat(new ChatEntity()
             {
                 ContactUsername = "Horn"
             });
             OnNewChat(new ChatEntity()
                 {
                     ContactUsername = "Admin"
                 }
             );
        }

        //public ChatPageViewModel()
        //{
        //    ChatList = new ChatListViewModel();

        //    OnNewChat(
        //        new ChatEntity()
        //        {
        //            ContactUsername = "Pussy"
        //        });

        //    OnNewChat(new ChatEntity()
        //    {
        //        ContactUsername = "Horn"
        //    });
        //    OnNewChat(new ChatEntity()
        //        {
        //            ContactUsername = "Admin"
        //        }
        //    );
        //}

        private void OnNewChat(ChatEntity chat)
        {
            var avatarViewModel = new AvatarViewModel
            (
                avatarLabel: chat.ContactUsername.Substring(0, 2)
            );

            UpdateColors(chat.ContactUsername, ref avatarViewModel);

            var chatViewModel = new ChatCardViewModel(chat.ContactUsername, avatarViewModel);

            Chats.Add(chatViewModel);
        }

        private void UpdateColors(string username, ref AvatarViewModel avatarViewModel)
        {
            int hash = username.GetStableHashCode(); // Convert.ToInt32(text); // string.GetHashCode(text);

            if (hash < 0)
                hash *= -1;

            int index = hash % _colors.Length;
            avatarViewModel.AvatarForeColor = new SolidColorBrush(_colors[index].ForeColor);
            avatarViewModel.AvatarFillColor = new SolidColorBrush(_colors[index].BackColor);
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
