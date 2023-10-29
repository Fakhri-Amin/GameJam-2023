using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public delegate void LevelStart(int levelID);
    public static event LevelStart onLevelStartEvent;
    public delegate void ChangeGameState(BattleSystem.State newState);
    public static event ChangeGameState onChangeGameStateEvent;

    public delegate void EnemyAttackPlayer(Damage damage);
    public static event EnemyAttackPlayer onEnemyAttackPlayerEvent;
    public delegate void EnemyCrashPlayer(Damage damage, UnitBase unit);
    public static event EnemyCrashPlayer onEnemyCrashPlayerEvent;
    public delegate void HealPlayer(Heal heal);
    public static event HealPlayer onHealPlayerEvent;
    public delegate void PlayerDamaged();
    public static event PlayerDamaged onPlayerDamagedEvent;
    public delegate void PlayerDead();
    public static event PlayerDead onPlayerDeadEvent;
    public delegate void LevelFinish();
    public static event LevelFinish onLevelFinishEvent;

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

    public void OnChangeGameState(BattleSystem.State newState)
    {
        onChangeGameStateEvent?.Invoke(newState);
    }

    public void OnEnemyAttackPlayer(Damage damage)
    {
        onEnemyAttackPlayerEvent?.Invoke(damage);
    }

    public void OnEnemyCrashPlayer(Damage damage, UnitBase unit)
    {
        onEnemyCrashPlayerEvent?.Invoke(damage, unit);
    }

    public void OnHealPlayer(Heal heal)
    {
        onHealPlayerEvent?.Invoke(heal);
    }

    public void OnPlayerDamaged()
    {
        onPlayerDamagedEvent?.Invoke();
    }

    public void OnPlayerDead()
    {
        onPlayerDeadEvent?.Invoke();
    }

    public void OnLevelFinish()
    {
        onLevelFinishEvent?.Invoke();
    }
}