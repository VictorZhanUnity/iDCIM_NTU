using UnityEngine;

namespace VictorDev.Common
{
    public static class ColorConverter
    {
        public static Color ConvertRgbToColor(float r, float g, float b)
        {
            return new Color(r / 255f, g / 255f, b / 255f);
        }

        public static Color colorOrange => ConvertRgbToColor(255, 125, 0);
        public static Color colorRed => ConvertRgbToColor(255, 50, 0);
    }
}
