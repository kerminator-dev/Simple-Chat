using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chat.Client.WPF.Controls
{
    /// <summary>
    /// Логика взаимодействия для MessageBubble.xaml
    /// </summary>
    public partial class MessageBubble : UserControl
    {
        public static readonly DependencyProperty MessageTextProperty = DependencyProperty.Register(
    "MessageText", typeof(string), typeof(MessageBubble),
    new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string MessageText
        {
            get { return (string)GetValue(MessageTextProperty); }
            set { SetValue(MessageTextProperty, value); }
        }

        public static readonly DependencyProperty MessageTextColorProperty = DependencyProperty.Register(
             "MessageTextColor",
             typeof(Brush),
             typeof(MessageBubble),
             new PropertyMetadata(Brushes.Black)
         );

        public Brush MessageTextColor
        {
            get { return (Brush)GetValue(MessageTextColorProperty); }
            set { SetValue(MessageTextColorProperty, value); }
        }

        public static readonly DependencyProperty MessageDateProperty = DependencyProperty.Register(
            "MessageDate",
            typeof(string),
            typeof(MessageBubble)
        );

        public string MessageDate
        {
            get => (string)GetValue(MessageDateProperty); 
            set => SetValue(MessageDateProperty, value);
        }

        public static readonly DependencyProperty MessageDateColorProperty = DependencyProperty.Register(
            "MessageDateColor",
            typeof(Brush),
            typeof(MessageBubble),
            new PropertyMetadata(Brushes.Black)
        );

        public Brush MessageDateColor
        {
            get { return (Brush)GetValue(MessageDateColorProperty); }
            set { SetValue(MessageDateColorProperty, value); }
        }

    public MessageBubble()
        {
            InitializeComponent();
        }
    }
}
