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

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<User> Users { get; set;  }
        public MainWindow()
        {
            InitializeComponent();

            Users = new List<User>();

            for (int i = 0; i < 40; i++)
            {
                Users.Add(new User() { IsOnline = true, Username = "Admin" });
                Users.Add(new User() { IsOnline = false, Username = "Emily" });
            }
            DataContext = this;
        }
    }
}
