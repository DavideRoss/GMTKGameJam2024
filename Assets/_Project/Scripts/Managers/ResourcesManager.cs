using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Resource
{
    Wood
}

public class ResourcesManager : MonoBehaviour
{
    static ResourcesManager _instance;
    public static ResourcesManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<ResourcesManager>();
            return _instance;
        }
    }

    [Header("Events")]
    public UnityEvent OnResourceUpdate;

    Dictionary<Resource, uint> _resources = new();

    public void Register(Resource resource, uint initialCount = 0)
    {
        _resources.Add(resource, initialCount);
        OnResourceUpdate.Invoke();
    }

    public void AddOrRegister(Resource resource, uint count)
    {
        if (_resources.ContainsKey(resource)) _resources[resource] += count;
        else _resources.Add(resource, count);

        OnResourceUpdate.Invoke();
    }

    public string GetResourceName(Resource resource)
    {
        switch (resource)
        {
            case Resource.Wood: return "Wood";
            default: throw new NotImplementedException("Missing resource name for " + resource.ToString());
        }
    }

    public Dictionary<string, uint> GetPrintableResources()
    {
        Dictionary<string, uint> dict = new();

        foreach (KeyValuePair<Resource, uint> kvp in _resources) dict.Add(GetResourceName(kvp.Key), kvp.Value);
        
        return dict;
    }
}