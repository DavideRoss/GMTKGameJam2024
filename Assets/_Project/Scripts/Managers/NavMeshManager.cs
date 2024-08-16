using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshManager : MonoBehaviour
{
    static NavMeshManager _instance;
    public static NavMeshManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<NavMeshManager>();
            return _instance;
        }
    }

    NavMeshSurface _surface;

    private void Start()
    {
        _surface = GetComponent<NavMeshSurface>();
    }

    public void UpdateNavMesh()
    {
        // TODO: test this
        _surface.UpdateNavMesh(_surface.navMeshData);
    }
}