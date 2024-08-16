using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    static BuildingManager _instance;
    public static BuildingManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<BuildingManager>();
            return _instance;
        }
    }

    readonly List<Building> _buildings = new();

    public void Register(Building building)
    {
        _buildings.Add(building);
    }

    public (Building, Workspot) GetNearestWorkspotOfType(BuildingType type, Vector3 point)
    {
        float currentDistance = Mathf.Infinity;
        Building currentBuilding = null;
        Workspot currentWorkspot = null;

        foreach (Building building in _buildings)
        {
            if (building.Type != type) continue;
            foreach (Workspot workspot in building.Workspots)
            {
                if (workspot.Occupied) continue;
                float dist = Vector3.Distance(building.transform.position + workspot.LocalPosition, point);
                if (dist < currentDistance)
                {
                    currentBuilding = building;
                    currentWorkspot = workspot;
                }
            }
        }

        return (currentBuilding, currentWorkspot);
    }
}