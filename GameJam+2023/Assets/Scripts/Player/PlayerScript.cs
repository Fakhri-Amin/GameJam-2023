using UnityEngine;

public class PlayerScript : MonoBehaviour, IDamageable
{
    [Header("Default Game Stats")]
    public float maxHealth = 100f;
    public float maxEnergy = 100f;

    [Header("Run Time Game Stats")]
    public float currentHealth;
    public float currentEnergy;

    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    private void Start()
    {
        EventManager.onLevelStartEvent += OnLevelStart;
        EventManager.onEnemyCrashPlayerEvent += OnEnemyAttackPlayer;
        EventManager.onPlayerDamagedEvent += OnPlayerDamaged;
        EventManager.instance.OnLevelStart(0);
    }

    private void OnDestroy()
    {
        EventManager.onLevelStartEvent -= OnLevelStart;
        EventManager.onEnemyCrashPlayerEvent -= OnEnemyAttackPlayer;
        EventManager.onPlayerDamagedEvent -= OnPlayerDamaged;
    }

    void OnLevelStart(int levelID)
    {
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
    }

    void OnEnemyAttackPlayer(UnitBase unit)
    {
        currentHealth -= unit.CrashDamage;
        EventManager.instance.OnPlayerDamaged();
    }

    void OnPlayerDamaged()
    {
        if (currentHealth <= 0)
        {
            Die();
            EventManager.instance.OnPlayerDead();
        }
    }

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        EventManager.instance.OnPlayerDamaged();
    }

    public void Die()
    {
        
    }
}