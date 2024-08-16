using UnityEngine;

namespace Kickass
{
    public static class Vector2Extension
    {
        public static Vector3 ToVector3(this Vector2 v, float y = 0) => new(v.x, y, v.y);
    }

    public static class Vector3Extension
    {
        public static Vector2Int ToVector2Int(this Vector3 v) => new(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.z));
        public static Vector2 ToVector2(this Vector3 v) => new(v.x, v.z);
        
        public static bool Approximately(this Vector3 v1, Vector3 v2) => Vector3.Distance(v1, v2) < float.Epsilon;
        public static bool Approximately(this Vector3 v1, Vector3 v2, float threshold = float.Epsilon) => Vector3.Distance(v1, v2) < threshold;
    }

    public static class Vector2IntExtension
    {
        public static Vector3 ToVector3(this Vector2Int v, float y = 0) => new(v.x, y, v.y);
        public static Vector2 ToVector2(this Vector2Int v) => new(v.x, v.y);
    }
}
