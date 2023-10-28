using UnityEngine;

public class LavaTile : MonoBehaviour
{
    public int enterDamage;
    public int activeDuration;

    int remainingDuration;

    private void Awake()
    {
        InstantiateTile();
    }

    private void Start()
    {
        EventManager.onChangeGameStateEvent += OnChangeGameState;
    }

    private void OnDestroy()
    {
        EventManager.onChangeGameStateEvent -= OnChangeGameState;
    }

    public void InstantiateTile()
    {
        remainingDuration = activeDuration;
        EnemySpawnManager.instance.MoveObject(gameObject);
    }

    public void OnChangeGameState(BattleSystem.State newState)
    {
        if (newState == BattleSystem.State.Waiting)
        {
            remainingDuration--;
            if (remainingDuration <= 0) DeactivateLavaTiles();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerScript>(out PlayerScript player))
        {
            var newDamage = new Damage() { damageValue = enterDamage };
            EventManager.instance.OnEnemyAttackPlayer(newDamage);
        }
    }

    void DeactivateLavaTiles()
    {
        EnemySpawnManager.instance.RemoveObject(gameObject);
        gameObject.SetActive(false);
    }
}