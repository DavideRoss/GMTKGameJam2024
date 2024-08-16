using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    public TMP_Text TextActionName;

    BaseAction _action;
    Interactable _interactable;

    public void InitializeButton(Interactable interactable, BaseAction action)
    {
        _interactable = interactable;
        _action = action;

        TextActionName.text = _action.ActionName;

        GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        _action.Execute(_interactable);
    }
}