using System.Windows.Media;

namespace Chat.Client.WPF.ViewModels
{
    internal class AvatarViewModel
    {
        public string AvatarLabel { get; }
        public Brush AvatarFillColor { get; set; }
        public Brush AvatarForeColor { get; set; }

        public AvatarViewModel(string avatarLabel)
        {
            AvatarLabel = avatarLabel;
        }
    }
}
