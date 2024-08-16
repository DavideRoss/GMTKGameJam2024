using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<GameManager>();
            return _instance;
        }
    }

    private void Start()
    {
        ResourcesManager.Instance.Register(Resource.Wood);
        UIManager.Instance.ForceUpdateResourceList();
    }
}