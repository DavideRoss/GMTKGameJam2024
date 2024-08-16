using System;

namespace Kickass
{
    public static class FloatExtension
    {
        public static bool Approximately(this float a, float b)
        {
            return Math.Abs(a - b) < float.Epsilon;
        }
    }
}