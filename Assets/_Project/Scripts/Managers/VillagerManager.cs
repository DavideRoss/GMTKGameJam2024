using System.Collections.Generic;
using UnityEngine;

public class VillagerManager : MonoBehaviour
{
    static VillagerManager _instance;
    public static VillagerManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<VillagerManager>();
            return _instance;
        }
    }

    public Villager VillagerPrefab;
    public Transform VillagerContainer;

    List<Villager> _villagers = new();

    public void Register(Villager villager)
    {
        _villagers.Add(villager);
    }

    public Villager SpawnVillager(Vector3 position)
    {
        return Instantiate(VillagerPrefab, position, Quaternion.identity, VillagerContainer);
    }
}