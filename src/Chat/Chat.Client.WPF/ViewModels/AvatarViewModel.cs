using System.Windows.Media;

namespace Chat.Client.WPF.ViewModels
{
    internal class AvatarViewModel
    {
        public string Label { get; set; }
        public int Hash { get; set; }
        public AvatarViewModel(string avatarLabel, int hash)
        {
            Label = avatarLabel;
            Hash = hash;
        }
    }
}
