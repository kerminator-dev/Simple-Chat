using System.Windows.Media;

namespace Chat.Client.WPF.ViewModels.UserControls
{
    internal class ChatCardViewModel : ViewModelBase
    {
        public AvatarViewModel AvatarViewModel { get; set; }

        public string ContactUsername { get; set; }
        public string AvatarLabel => AvatarViewModel.Label;

        public string ContentPreview { get; set; } = "No messages";

        public ChatCardViewModel(string contactUserame, AvatarViewModel avatarVeiwModel)
        {
            AvatarViewModel = avatarVeiwModel;
            ContactUsername = contactUserame;
            
        }
    }
}
