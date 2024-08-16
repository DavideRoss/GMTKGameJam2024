using UnityEngine;

public class UIManager : MonoBehaviour
{
    static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<UIManager>();
            return _instance;
        }
    }

    public OverlayActions OverlayActions;

    public void ShowActionsOverlay(Interactable interactable)
    {
        OverlayActions.gameObject.SetActive(true);
        OverlayActions.SpawnActions(interactable);
    }

    public void HideActionsOverlay()
    {
        OverlayActions.ClearActions();
        OverlayActions.gameObject.SetActive(false);
    }
}