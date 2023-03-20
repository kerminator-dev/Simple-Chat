using System.Linq;
using System.Windows.Media;

namespace Chat.Client.WPF.ViewModels
{
    internal class ChatItemViewModel : ViewModelBase
    {
        public string Username { get; set; }

        public string ContentPreview { get; set; }

        public char AvatarText { get => char.ToUpper(Username.FirstOrDefault()); }

        public Brush AvatarFillColor { get; set; }
        public Brush AvatarForeColor { get; set; }
    }
}
