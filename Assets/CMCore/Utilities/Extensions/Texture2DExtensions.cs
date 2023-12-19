using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CMCore.Utilities.Extensions
{
    public static class Texture2DExtensions
    {
        public static Color GetDominantColor(this Texture2D texture, bool countAlpha = false)
        {
            var pixels = texture.GetPixels();
            var colorCount = new Dictionary<Color, int>();

            // Count the frequency of each color
            foreach (var pixel in pixels)
            {
                if (colorCount.ContainsKey(pixel))
                {
                    colorCount[pixel]++;
                }
                else
                {
                    if (!countAlpha && pixel.a <1 ) continue;
                    colorCount.Add(pixel, 1);
                }
            }

            // Find the color with the highest frequency
            var dominantColor = Color.clear;
            var maxCount = 0;

            foreach (var color in colorCount.Where(color => color.Value > maxCount))
            {
                dominantColor = color.Key;
                maxCount = color.Value;
            }

            return dominantColor;
        }
    }
}