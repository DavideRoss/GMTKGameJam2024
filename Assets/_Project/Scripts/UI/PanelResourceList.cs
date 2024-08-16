using System.Collections.Generic;
using UnityEngine;

public class PanelResourceList : MonoBehaviour
{
    public PanelResource PanelResourcePrefab;

    private void Start()
    {
        ResourcesManager.Instance.OnResourceUpdate.AddListener(UpdateList);
    }

    public void UpdateList()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);

        foreach (KeyValuePair<string, uint> kvp in ResourcesManager.Instance.GetPrintableResources())
        {
            PanelResource newPanel = Instantiate(PanelResourcePrefab, transform);
            newPanel.InitializePanel(kvp.Key, kvp.Value);
        }
    }
}