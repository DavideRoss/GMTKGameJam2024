using System;
using UnityEngine;

namespace Kickass
{
    public static class MatrixExtension
    {
        public static Vector2Int CoordinatesOf<T>(this T[,] matrix, T value)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (matrix[x, y] != null && matrix[x, y].Equals(value)) return new Vector2Int(x, y);
                }
            }

            return new Vector2Int(-1, -1);
        }

        public static Vector2Int CoordinatesOf<T>(this T[,] matrix, Type type)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (matrix[x, y] != null && matrix[x, y].GetType() == type) return new Vector2Int(x, y);
                }
            }

            return new Vector2Int(-1, -1);
        }
    }
}