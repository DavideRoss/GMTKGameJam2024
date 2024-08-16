using UnityEngine;
using UnityEditor;
using Kickass;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    public List<BaseAction> Actions;

    public void OnPointed()
    {
        Debug.Log("[Interactable] " + "Pointed!");

        
    }

    public void OnUnpointed()
    {
        Debug.Log("[Interactable] " + "Unpointed!");
    }

    public void OnSelected()
    {
        UIManager.Instance.ShowActionsOverlay(this);
    }

    public void OnUnselected()
    {
        UIManager.Instance.HideActionsOverlay();
    }

    #if UNITY_EDITOR

    private void InitializeBoxCollider()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactables");

        BoxCollider coll = GetComponent<BoxCollider>();

        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length > 0)
        {
            Bounds bounds = new();
            foreach (Renderer renderer in renderers)
            {
                Bounds tempBounds = renderer.localBounds;
                tempBounds.center += renderer.transform.localPosition;
                bounds.Encapsulate(tempBounds);
            }

            coll.center = bounds.center;
            coll.size = bounds.size;
        }
        else
        {
            Debug.LogWarning("[Interactable] " + "Cannot generate box collider bounds");
        }
    }

    [CustomEditor(typeof(Interactable))]
    public class InteractableEditor : Editor
    {
        Interactable _interactable;

        private void OnEnable() => _interactable = target as Interactable;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUIPlus.Separator();

            if (GUILayout.Button("Initialize box collider")) _interactable.InitializeBoxCollider();
        }
    }

    #endif
}
