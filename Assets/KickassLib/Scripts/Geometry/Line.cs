using UnityEngine;

namespace Kickass.Geometry
{
    public static class Line
    {
        public static bool TryLineIntersection(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2, out Vector2 intersection)
        {
            intersection = Vector2.zero;

            float dx = a2.x - a1.x;
            float dy = a2.y - a1.y;
            float da = b2.x - b1.x;
            float db = b2.y - b1.y;

            float determinant = da * dy - db * dx;

            if (determinant.Approximately(0)) return false;

            float s = (dx * (b1.y - a1.y) + dy * (a1.x - b1.x)) / determinant;
            float t = (da * (a1.y - b1.y) + db * (b1.x - a1.x)) / -determinant;
            
            if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
            {
                intersection = new Vector2(
                    a1.x + t * dx,
                    a1.y + t * dy
                );
                return true;
            }

            return false;
        }

        public static bool TryLineIntersection(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2) => TryLineIntersection(a1, a2, b1, b2, out _);
    }
}