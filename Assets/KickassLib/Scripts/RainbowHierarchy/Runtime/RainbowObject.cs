using UnityEditor;
using UnityEngine;

namespace Kickass.RainbowHierarchy
{
    [DisallowMultipleComponent]
    public class RainbowObejct : MonoBehaviour
    {
        #if UNITY_EDITOR

        public Color BackgroundColor;
        public Color TextColor;

        private void OnValidate()
        {
            EditorApplication.RepaintHierarchyWindow();
        }

        #endif
    }
}
