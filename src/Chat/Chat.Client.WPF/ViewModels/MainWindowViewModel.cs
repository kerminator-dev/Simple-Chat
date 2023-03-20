using Chat.Client.WPF.Models;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Chat.Client.WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<ChatItemViewModel> Chats { get; set; }
        public ChatItemViewModel Chat { get; set; } = new ChatItemViewModel()
        {
            Username = "Admin",
            AvatarFillColor = new SolidColorBrush(Color.FromRgb(229, 228, 255)),
            AvatarForeColor = new SolidColorBrush(Color.FromRgb(132, 121, 208)),
            ContentPreview = "some message here"
        };

        public MainWindowViewModel()
        {
            Chats = new ObservableCollection<ChatItemViewModel>
            {
                new ChatItemViewModel()
                {
                    Username = "Admin",
                    AvatarFillColor = new SolidColorBrush(Color.FromRgb(229, 228, 255)),
                    AvatarForeColor = new SolidColorBrush(Color.FromRgb(132, 121, 208)),
                    ContentPreview = "some message here"
                },

                new ChatItemViewModel()
                {
                    AvatarFillColor = new SolidColorBrush(Color.FromRgb(220, 243, 255)),
                    AvatarForeColor = new SolidColorBrush(Color.FromRgb(137, 207, 236)),
                    Username = "Pussy",
                    ContentPreview = "some message here"
                }
                ,
                new ChatItemViewModel()
                {
                    Username = "Gray",
                    AvatarFillColor = new SolidColorBrush(Color.FromRgb(246, 229, 214)),
                    AvatarForeColor = new SolidColorBrush(Color.FromRgb(231, 160, 114)),
                    ContentPreview = "some message here"
                },

                new ChatItemViewModel()
                {
                    AvatarFillColor = new SolidColorBrush(Color.FromRgb(249, 228, 245)),
                    AvatarForeColor = new SolidColorBrush(Color.FromRgb(200, 119, 159)),
                    Username = "Dan",
                    ContentPreview = "some message here"
                }
            };
        }
    }
}
