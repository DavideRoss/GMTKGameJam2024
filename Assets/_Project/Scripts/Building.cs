using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    House
}

[System.Serializable]
public class Workspot
{
    public bool Occupied;
    public Vector3 LocalPosition;
}

public class Building : MonoBehaviour
{
    public BuildingType Type;
    public List<Workspot> Workspots;

    private void Start()
    {
        BuildingManager.Instance.Register(this);
    }

    #if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        foreach (Workspot spot in Workspots)
        {
            Gizmos.color = spot.Occupied ? Color.red : Color.green;
            Gizmos.DrawSphere(spot.LocalPosition, 0.25f);
        }

        Gizmos.matrix = Matrix4x4.identity;
    }

    #endif
}