using System.Windows.Media;

namespace Chat.Client.WPF.Models
{
    internal class AvatarColors
    {
        public Color ForeColor { get; set; }
        public Color BackColor { get; set; }

        public AvatarColors(Color backColor, Color foreColor)
        {
            BackColor = backColor;
            ForeColor = foreColor;
        }
    }
}
