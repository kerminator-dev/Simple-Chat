using Chat.Client.WPF.Models;
using System;
using System.Windows.Media;

namespace Chat.Client.WPF.Services
{
    internal static class AvatarColorsStore
    {
        private static readonly AvatarColors[] _colors = new AvatarColors[]
        {
            new AvatarColors(backColor: Color.FromRgb(248, 230, 242), foreColor: Color.FromRgb(195, 120, 164)),
            new AvatarColors(backColor: Color.FromRgb(227,229,254),   foreColor: Color.FromRgb(140,135,223)),
            new AvatarColors(backColor: Color.FromRgb(219,243,252),   foreColor: Color.FromRgb(116,188,218)),
            new AvatarColors(backColor: Color.FromRgb(249,231,215),   foreColor: Color.FromRgb(230, 159, 92)),
            new AvatarColors(backColor: Color.FromRgb(122,204,70),   foreColor: Color.FromRgb(23,113,62)),
            new AvatarColors(backColor: Color.FromRgb(203, 228, 249),   foreColor: Color.FromRgb(58, 164, 249)),

            new AvatarColors(backColor: Color.FromRgb(203, 249, 212),   foreColor: Color.FromRgb(135, 243, 156)),
            new AvatarColors(backColor: Color.FromRgb(245, 242, 194),   foreColor: Color.FromRgb(243, 239, 135)),
            new AvatarColors(backColor: Color.FromRgb(245, 194, 194),   foreColor: Color.FromRgb(253, 146, 146)),
        };

        public static int Count => _colors.Length;

        public static Color GetBackColor(int index)
        {
            if (index < 0 && index >= _colors.Length)
                throw new IndexOutOfRangeException();

            return _colors[index].BackColor;
        }

        public static Color GetForeColor(int index)
        {
            if (index < 0 && index >= _colors.Length)
                throw new IndexOutOfRangeException(); 
            
            return _colors[index].ForeColor;
        }
    }
}
