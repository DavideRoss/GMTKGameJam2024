using UnityEngine;

public class OverlayActions : MonoBehaviour
{
    public ButtonAction ButtonActionPrefab;

    Interactable _selected;

    public void SpawnActions(Interactable interactable)
    {
        _selected = interactable;

        foreach(BaseAction action in _selected.Actions)
        {
            ButtonAction newButton = Instantiate(ButtonActionPrefab, transform);
            newButton.InitializeButton(_selected, action);
        }
    }

    public void ClearActions()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}