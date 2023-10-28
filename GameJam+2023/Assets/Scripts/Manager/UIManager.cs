using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public UIPlayerStats playerStats;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }


}