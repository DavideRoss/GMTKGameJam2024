using UnityEngine;


[CreateAssetMenu(fileName = "Action_StokeFire", menuName = "Village/Actions/01. Stoke Fire")]
public class ActionStokeFire : BaseAction
{
    public override void Execute(Interactable interactable)
    {
        Debug.Log("[StokeFire] " + "Clicked on stoke fire");
    }
}