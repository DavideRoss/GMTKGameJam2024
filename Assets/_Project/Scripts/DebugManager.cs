using UnityEngine;

public class DebugManager : MonoBehaviour
{
    static DebugManager _instance;
    public static DebugManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<DebugManager>();
            return _instance;
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.LogWarning("[DebugManager] " + "Used debug keystroke!");
            SpawnVillager();
        }
    }

    public void SpawnVillager()
    {
        Villager newVillager = VillagerManager.Instance.SpawnVillager(Vector3.zero);
        newVillager.SetOrder(new WorkToNearestWorkspot(BuildingType.House));
    }
}