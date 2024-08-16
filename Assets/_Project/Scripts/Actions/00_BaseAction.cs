using UnityEngine;

public class BaseAction : ScriptableObject
{
    public string ActionName;

    public virtual void Execute(Interactable interactable) {}
}