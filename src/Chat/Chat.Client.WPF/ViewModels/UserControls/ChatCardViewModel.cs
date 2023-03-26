using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Media;

namespace Chat.Client.WPF.ViewModels.UserControls
{
    internal class ChatCardViewModel : ViewModelBase
    {
        public AvatarViewModel Avatar { get; set; }

        public string ContactUsername { get; set; }

        public string ContentPreview { get; set; } = "No messages";

        public ObservableCollection<MessageViewModel> Messages { get; set; }

        public ChatCardViewModel(string contactUserame, AvatarViewModel avatarVeiwModel)
        {
            Avatar = avatarVeiwModel;
            ContactUsername = contactUserame;
            Messages = new ObservableCollection<MessageViewModel>();
        }
    }
}
