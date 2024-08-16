using UnityEngine;

namespace Kickass
{
    public static class ColorExtension
    {
        public static Color Lighten(this Color color, float percentage)
        {
            float delta = color.grayscale * percentage;
            return new Color(color.r + delta, color.g + delta, color.b + delta);
        }

        public static Color Darken(this Color color, float percentage)
        {
            return color.Lighten(-percentage);
        }

        public static string ToHex(this Color c)
        {
            return "#" + ColorUtility.ToHtmlStringRGB(c);
        }
    }

    public static class RandomColor
    {
        public static Color HSV(float saturation = 1, float value = 1)
        {
            return Color.HSVToRGB(Random.value, saturation, value);
        }
    }
}
