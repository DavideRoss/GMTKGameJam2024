using UnityEngine;

public class WorkToNearestWorkspot : BaseOrder
{
    BuildingType _type;

    public WorkToNearestWorkspot(BuildingType type)
    {
        _type = type;
    }

    public override void Execute(Villager villager)
    {
        (Building building, Workspot workspot) = BuildingManager.Instance.GetNearestWorkspotOfType(_type, villager.transform.position);
        if (building == null)
        {
            Debug.LogWarning("[WorkToNearestWorkspot] " + "Impossible to find a workspot");
            return;
        }

        workspot.Occupied = true;

        villager.MoveTo(building.transform.TransformPoint(workspot.LocalPosition));
    }
}
