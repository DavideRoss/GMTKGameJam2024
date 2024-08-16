using UnityEngine;
using TMPro;

public class PanelResource : MonoBehaviour
{
    public TMP_Text TextResourceName;
    public TMP_Text TextResourceCount;

    public void InitializePanel(string resourceName, uint count)
    {
        TextResourceName.text = resourceName;
        TextResourceCount.text = count.ToString();
    }
}