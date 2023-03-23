using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Chat.Client.WPF.Extensions
{
    internal static class ColorExtensions
    {
        /// <summary>
        /// Creates color with corrected brightness.
        /// </summary>
        /// <param name="color">Color to correct.</param>
        /// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1. 
        /// Negative values produce darker colors.</param>
        /// <returns>
        /// Corrected <see cref="Color"/> structure.
        /// </returns>
        public static Color ChangeColorBrightness(this Color color, float correctionFactor)
        {
            if (correctionFactor == 0f)
            {
                return color;
            }

            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (byte)red, (byte)green, (byte)blue);
        }

        public static Color Darken(this Color c, float level)
        {
            // 'level' is how dark to make the color.
            // Each RGB component is divided by 'level'.
            return Color.FromArgb(c.A,
              (byte)(c.R / level), (byte)(c.G / level), (byte)(c.B / level));
        }

    }
}
