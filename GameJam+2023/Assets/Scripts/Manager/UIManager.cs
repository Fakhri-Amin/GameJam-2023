using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public UIPlayerStats playerStats;
    public UIPlayerSkills playerSkills;
    public UIUpgradePanel upgradePanel;

    public GameObject WinCutscene;
    public GameObject LoseCutscene;

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

    private void Start()
    {
        EventManager.onGameFinishEvent += PlayWinCutscene;
        EventManager.onPlayerDeadEvent += PlayLosePanel;
    }

    private void OnDestroy()
    {
        EventManager.onGameFinishEvent -= PlayWinCutscene;
        EventManager.onPlayerDeadEvent -= PlayLosePanel;
    }

    public void PlayWinCutscene()
    {
        WinCutscene.SetActive(true);
    }

    public void PlayLosePanel()
    {
        LoseCutscene.SetActive(true);
    }

}