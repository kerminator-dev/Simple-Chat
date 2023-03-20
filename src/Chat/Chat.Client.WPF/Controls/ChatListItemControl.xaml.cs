using System.Windows;
using System.Windows.Controls;

namespace Chat.Client.WPF.Controls
{
    /// <summary>
    /// Логика взаимодействия для ChatListItemControl.xaml
    /// </summary>
    public partial class ChatListItemControl : UserControl
    {
        public static readonly DependencyProperty UsernameProperty =
    DependencyProperty.Register("Username", typeof(string), typeof(AvatarControl));

        public string Username
        {
            get => (string)GetValue(UsernameProperty);
            set => SetValue(UsernameProperty, value);
        }

        public static readonly DependencyProperty ContentPreviewProperty =
    DependencyProperty.Register("ContentPreview", typeof(string), typeof(AvatarControl));

        public string ContentPreview
        {
            get => (string)GetValue(ContentPreviewProperty);
            set => SetValue(ContentPreviewProperty, value);
        }

        public ChatListItemControl()
        {
            InitializeComponent();
        }
    }
}
