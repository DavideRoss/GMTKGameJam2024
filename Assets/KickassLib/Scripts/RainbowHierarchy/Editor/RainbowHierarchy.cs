using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;

using Object = UnityEngine.Object;

namespace Kickass.RainbowHierarchy
{
    [InitializeOnLoad]
    public static class RainbowHierarchy
    {
        static RainbowHierarchy()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
        }

        private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            Object instance = EditorUtility.InstanceIDToObject(instanceID);
            if (instance == null) return;

            if ((instance as GameObject).TryGetComponent(out RainbowObejct rainbowObject))
            {
                Rect backgroundRect = GetBackgroundRect(selectionRect);
                Color backgroundColor = rainbowObject.BackgroundColor;

                // if (Selection.Contains(instanceID)) backgroundColor = backgroundColor.Darken(.25f);
                if (backgroundRect.Contains(Event.current.mousePosition)) backgroundColor = backgroundColor.Lighten(.25f);

                EditorGUI.DrawRect(backgroundRect, backgroundColor);

                GUIStyle labelGUIStyle = new()
                {
                    normal = new GUIStyleState { textColor = rainbowObject.TextColor }
                };

                EditorGUI.LabelField(GetTextRect(selectionRect), rainbowObject.name, labelGUIStyle);

                if (rainbowObject.transform.childCount > 0)
                {
                    Type sceneHierarchyWindowType = typeof(Editor).Assembly.GetType("UnityEditor.SceneHierarchyWindow");
                    PropertyInfo sceneHierarchyWindow = sceneHierarchyWindowType.GetProperty("lastInteractedHierarchyWindow", BindingFlags.Public | BindingFlags.Static);

                    int[] expandedIDs = (int[])sceneHierarchyWindowType.GetMethod("GetExpandedIDs", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(sceneHierarchyWindow.GetValue(null), null);

                    string iconID = expandedIDs.Contains(instanceID) ? "IN Foldout on" : "IN foldout";

                    GUI.DrawTexture(GetCollapseToggleIconRect(selectionRect), EditorGUIUtility.IconContent(iconID).image, ScaleMode.StretchToFill, true, 0f, Color.white, 0f, 0f);
                }
            }
        }

        private static Rect GetBackgroundRect(Rect selectionRect)
        {
            float xPos = selectionRect.position.x + 60f - 28f - selectionRect.xMin;
            float yPos = selectionRect.position.y;
            float xSize = selectionRect.size.x + selectionRect.xMin + 28f - 60 + 16f;
            float ySize = selectionRect.size.y;
            return new Rect(xPos, yPos, xSize, ySize);
        }

        public static Rect GetTextRect(Rect selectionRect)
        {
            float xPos = selectionRect.position.x + 18f;
            float yPos = selectionRect.position.y;
            float xSize = selectionRect.size.x - 18f;
            float ySize = selectionRect.size.y;
            return new Rect(xPos, yPos, xSize, ySize);
        }

        public static Rect GetCollapseToggleIconRect(Rect selectionRect)
        {
            float xPos = selectionRect.position.x - 14f;
            float yPos = selectionRect.position.y + 1f;
            float xSize = 13f;
            float ySize = 13f;
            return new Rect(xPos, yPos, xSize, ySize);
        }
    }
}
