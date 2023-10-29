using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public UIPlayerStats playerStats;
    public UIPlayerSkills playerSkills;

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