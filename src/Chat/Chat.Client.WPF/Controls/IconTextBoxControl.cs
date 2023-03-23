using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    ///     <MyNamespace:IconTextBoxControl/>
    ///
    /// </summary>
    public class IconTextBoxControl : Control
    {
        public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(PackIconMaterialKind), typeof(IconTextBoxControl));

        public PackIconMaterialKind Icon
        {
            get => (PackIconMaterialKind)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(IconTextBoxControl));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty IconForegroundProperty =
        DependencyProperty.Register("IconForeground", typeof(Brush), typeof(IconTextBoxControl));

        public Brush IconForeground
        {
            get => (Brush)GetValue(IconForegroundProperty);
            set => SetValue(IconForegroundProperty, value);
        }

        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(double), typeof(IconTextBoxControl));

        [TypeConverterAttribute(typeof(LengthConverter))]
        public double IconSize
        {
            get => (double)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }

        static IconTextBoxControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconTextBoxControl), new FrameworkPropertyMetadata(typeof(IconTextBoxControl)));
        }
    }
}
