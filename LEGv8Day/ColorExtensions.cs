using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEGv8Day
{
    internal static class ColorExtensions
    {
        public static Color GetTextColor(this Color color)
        {
            return color.GetBrightness() <= 0.5f ? Color.White : Color.Black;
        }
    }
}
