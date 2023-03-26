using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Chat.Client.WPF.Controls
{
    /// <summary>
    /// Выполните шаги 1a или 1b, а затем 2, чтобы использовать этот пользовательский элемент управления в файле XAML.
    ///
    /// Шаг 1a. Использование пользовательского элемента управления в файле XAML, существующем в текущем проекте.
    /// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    /// будет использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Chat.Client.WPF.Controls"
    ///
    ///
    /// Шаг 1б. Использование пользовательского элемента управления в файле XAML, существующем в другом проекте.
    /// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    /// будет использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Chat.Client.WPF.Controls;assembly=Chat.Client.WPF.Controls"
    ///
    /// Потребуется также добавить ссылку из проекта, в котором находится файл XAML,
    /// на данный проект и пересобрать во избежание ошибок компиляции:
    ///
    ///     Щелкните правой кнопкой мыши нужный проект в обозревателе решений и выберите
    ///     "Добавить ссылку"->"Проекты"->[Поиск и выбор проекта]
    ///
    ///
    /// Шаг 2)
    /// Теперь можно использовать элемент управления в файле XAML.
    ///
    ///     <MyNamespace:Message/>
    ///
    /// </summary>
    public class Message : ListBoxItem
    {
        public static readonly DependencyProperty TextProperty =
           DependencyProperty.Register("Text", typeof(string), typeof(Message));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(string), typeof(Message));

        public string Time
        {
            get => (string)GetValue(TimeProperty);
            set => SetValue(TimeProperty, value);
        }

        public static readonly DependencyProperty SecondaryBackgroundProperty =
            DependencyProperty.Register("SecondaryBackground", typeof(Brush), typeof(Message));

        public Brush SecondaryBackground
        {
            get => (Brush)GetValue(SecondaryBackgroundProperty);
            set => SetValue(SecondaryBackgroundProperty, value);
        }

        public static readonly DependencyProperty TimeForegroundProperty =
    DependencyProperty.Register("TimeForeground", typeof(Brush), typeof(Message));

        public Brush TimeForeground
        {
            get => (Brush)GetValue(TimeForegroundProperty);
            set => SetValue(TimeForegroundProperty, value);
        }

        static Message()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Message), new FrameworkPropertyMetadata(typeof(Message)));
        }
    }
}
