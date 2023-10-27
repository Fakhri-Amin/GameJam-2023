using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public delegate void LevelStart(int levelID);
    public static event LevelStart onLevelStartEvent;
    public delegate void EnemyCrashPlayer(UnitBase unit);
    public static event EnemyCrashPlayer onEnemyCrashPlayerEvent;
    public delegate void PlayerDamaged();
    public static event PlayerDamaged onPlayerDamagedEvent;
    public delegate void PlayerDead();
    public static event PlayerDead onPlayerDeadEvent;

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

    public void OnLevelStart(int levelID)
    {
        onLevelStartEvent?.Invoke(levelID);
    }

    public void OnEnemyCrashPlayer(UnitBase unit)
    {
        onEnemyCrashPlayerEvent?.Invoke(unit);
    }

    public void OnPlayerDamaged()
    {
        onPlayerDamagedEvent?.Invoke();
    }

    public void OnPlayerDead()
    {
        onPlayerDeadEvent?.Invoke();
    }
}