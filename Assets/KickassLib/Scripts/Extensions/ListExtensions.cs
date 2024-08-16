using System.Collections.Generic;
using UnityEngine;

namespace Kickass
{
    public static class ListExtension
    {
        public static void Shuffle<T>(this List<T> list)
        {
            int index = list.Count;
            while (index > 1)
            {
                index--;
                int newIndex = Random.Range(0, index + 1);
                (list[newIndex], list[index]) = (list[index], list[newIndex]);
            }
        }

        public static T Pick<T>(this List<T> list)
        {
            int lastIndex = list.Count - 1;
            T el = list[lastIndex];
            list.RemoveAt(lastIndex);
            return el;
        }

        public static List<T> Pick<T>(this List<T> list, int count = 1)
        {
            List<T> output = new();

            for (int i = 0; i < count; i++) output.Add(list.Pick());

            return output;
        }

        public static bool PickIfNotEmpty<T>(this List<T> list, out List<T> output, int count = 1)
        {
            output = new List<T>();

            for (int i = 0; i < count; i++)
            {
                if (list.Count == 0) return false;
                int lastIndex = list.Count - 1;
                output.Add(list[lastIndex]);
                list.RemoveAt(lastIndex);
            }

            return true;
        }

        public static T Pop<T>(this List<T> list)
        {
            if (list.Count == 0) throw new System.Exception("List has no entries");
            T r = list[0];
            list.RemoveAt(0);
            return r;
        }
    }
}
