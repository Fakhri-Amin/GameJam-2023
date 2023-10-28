using UnityEngine;

public class PlayerScript : MonoBehaviour, IDamageable
{
    [Header("Default Game Stats")]
    public float maxHealth = 100f;
    public float maxEnergy = 100f;

    [Header("Run Time Game Stats")]
    float currentHealth;
    float currentEnergy;

    public float CurrentHealth 
    { 
        get {return currentHealth; } 
        set { currentHealth = value; UIManager.instance.playerStats.UpdateHealth(currentHealth, maxHealth); } 
    }
    public float CurrentEnergy
    {
        get { return currentEnergy; }
        set { currentEnergy = value; UIManager.instance.playerStats.UpdateEnergy(currentEnergy, maxEnergy); }
    }
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
        CurrentHealth = maxHealth;
        CurrentEnergy = maxEnergy;
    }

    void OnEnemyAttackPlayer(UnitBase unit)
    {
        CurrentHealth -= unit.CrashDamage;
        EventManager.instance.OnPlayerDamaged();
    }

    void OnPlayerDamaged()
    {
        if (CurrentHealth <= 0)
        {
            Die();
            EventManager.instance.OnPlayerDead();
        }
    }

    public void Damage(Damage damage)
    {
        CurrentHealth -= damage.damageValue;
        EventManager.instance.OnPlayerDamaged();
    }

    public void Die()
    {
        
    }

}