using UnityEngine;

namespace CMCore.Utilities.Extensions
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Converts a uint to a color
        /// </summary>
        /// <param name="color">The uint to convert</param>
        /// <returns></returns>
        public static uint ToUInt(this Color color)
        {
            Color32 c32 = color;
            return (uint)((c32.a << 24) | (c32.r << 16) |
                          (c32.g << 8) | (c32.b << 0));
        }

        /// <summary>
        /// Converts a uint to a color32
        /// </summary>
        /// <param name="color">The uint to convert</param>
        /// <returns></returns>
        public static Color32 ToColor32(this uint color)
        {
            return new Color32()
            {
                a = (byte)(color >> 24),
                r = (byte)(color >> 16),
                g = (byte)(color >> 8),
                b = (byte)(color >> 0)
            };
        }

        /// <summary>
        /// Converts a uint to a color
        /// </summary>
        /// <param name="color">The uint to convert</param>
        /// <returns></returns>
        public static Color ToColor(this uint color)
        {
            return ToColor32(color);
        }

        /// <summary>
        /// Converts a color to its hex counterpart
        /// </summary>
        /// <param name="color">The color to convert</param>
        /// <returns></returns>
        public static string ToHex(this Color32 color)
        {
            return "#" + color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        }
        
        public static string ToHex(this Color color)
        {
            return "#" + ((int)(color.r * 255f)).ToString("X2") + ((int)(color.g * 255f)).ToString("X2") + ((int)(color.b * 255f)).ToString("X2");
        }

        /// <summary>
        /// Converts a color to its hex counterpart
        /// </summary>
        /// <param name="hexCode"></param>
        /// <returns></returns>
        public static Color ToColor(this string hexCode)
        {
            var hexFinal = hexCode;
            if (hexCode[0] != '#')
                hexFinal = "#" + hexCode;
            return ColorUtility.TryParseHtmlString(hexFinal, out var color) ? color : Color.black;
        }

    }
}
