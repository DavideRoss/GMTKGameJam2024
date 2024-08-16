using UnityEngine;

[CreateAssetMenu(fileName = "Action_GatherKindling", menuName = "Village/Actions/02. Gather Kindling")]
public class ActionGatherKindling : BaseAction
{
    public override void Execute(Interactable interactable)
    {
        Debug.Log("[GatherKindling] " + "Clicked on kindling");
    }
}