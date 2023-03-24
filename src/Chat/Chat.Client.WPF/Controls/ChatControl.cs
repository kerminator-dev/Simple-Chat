using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Chat.Client.WPF.Controls
{
    public class ChatControl : ListBoxItem
    {
        public static readonly DependencyProperty AvatarProperty =
      DependencyProperty.Register("Avatar", typeof(Avatar), typeof(ChatControl));

        public Avatar Avatar
        {
            get => (Avatar)GetValue(AvatarProperty);
            set => SetValue(AvatarProperty, value);
        }

        public static readonly DependencyProperty ContactUsernameProperty =
   DependencyProperty.Register("ContactUsername", typeof(string), typeof(ChatControl));

        public string ContactUsername
        {
            get => (string)GetValue(ContactUsernameProperty);
            set => SetValue(ContactUsernameProperty, value);
        }

        public static readonly DependencyProperty ContentPreviewProperty =
   DependencyProperty.Register("ContentPreview", typeof(string), typeof(ChatControl));

        public string ContentPreview
        {
            get => (string)GetValue(ContentPreviewProperty);
            set => SetValue(ContentPreviewProperty, value);
        }

        public Brush SelectedBackColor
        {
            get { return (Brush)GetValue(SelectedBackColorProperty); }
            set { SetValue(SelectedBackColorProperty, value); }
        }

        public static readonly DependencyProperty SelectedBackColorProperty =
            DependencyProperty.Register("SelectedBackColor", typeof(Brush), typeof(ChatControl), new PropertyMetadata(new SolidColorBrush()));


        static ChatControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChatControl), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true,
                DefaultValue = false
            });
        }
    }
}
