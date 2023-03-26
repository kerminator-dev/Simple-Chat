using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Client.WPF.ViewModels.UserControls
{
    internal class MessageViewModel
    {
        public int Id { get; set; }
        public string OutputText { get; set; }
        public bool IsFromMe { get; set; }
        public string Time { get; set; }
    }
}
