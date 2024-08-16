using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    static InputManager _instance;
    public static InputManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<InputManager>();
            return _instance;
        }
    }

    Interactable _lastPointed;
    Interactable _lastSelected;
    
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Interactables")))
        {
            Interactable pointedObject = hit.collider.gameObject.GetComponent<Interactable>();
            if (pointedObject != null)
            {
                if (_lastPointed != pointedObject)
                {
                    if (_lastPointed != null) _lastPointed.OnUnpointed();
                    pointedObject.OnPointed();
                    _lastPointed = pointedObject;
                }
            }
        }
        else
        {
            if (_lastPointed != null) _lastPointed.OnUnpointed();
            _lastPointed = null;
        }

        if (Input.GetMouseButtonDown(0) && _lastPointed != null)
        {
            if (_lastSelected != null) _lastSelected.OnUnselected();
            _lastSelected = _lastPointed;
            _lastSelected.OnSelected();
        }
    }
}