using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

namespace Kickass
{
    public static class EditorGUIPlus
    {
        public static void Separator(int height = 1, int space = 8)
        {
            GUILayout.Space(space);
        
            Rect rect = GUILayoutUtility.GetRect(10, height, GUILayout.ExpandWidth(true));
            rect.height = height;
            rect.xMin = 20;
            rect.xMax = EditorGUIUtility.currentViewWidth - 10;
        
            Color lineColor = new(0.10196f, 0.10196f, 0.10196f, 1);
            EditorGUI.DrawRect(rect, lineColor);
            GUILayout.Space(space);
        }
    }
}

#endif