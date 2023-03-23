using Chat.Client.WPF.Models;
using System.Windows.Media;

namespace Chat.Client.WPF.ViewModels.UserControls
{
    internal class ChatCardViewModel : ViewModelBase
    {
        private readonly AvatarViewModel _avatarViewModel;

        public string ContactUsername { get; set; }
        public string AvatarLabel => _avatarViewModel.AvatarLabel;

        public string ContentPreview { get; set; } = "No messages";

        public Brush AvatarFillColor => _avatarViewModel.AvatarFillColor;
        public Brush AvatarForeColor => _avatarViewModel.AvatarForeColor;

        public ChatCardViewModel(string contactUserame, AvatarViewModel avatarVeiwModel)
        {
            _avatarViewModel = avatarVeiwModel;
            ContactUsername = contactUserame;
            
        }
    }
}
