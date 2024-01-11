using UnityEngine;

public class LavaTile : MonoBehaviour
{
    public int enterDamage;
    public int activeDuration;

    int remainingDuration;
    CampaignTilesScript tilesScript;

    private void Awake()
    {

    }

    private void Start()
    {
        EventManager.onChangeGameStateEvent += OnChangeGameState;
    }

    private void OnDestroy()
    {
        EventManager.onChangeGameStateEvent -= OnChangeGameState;
    }

    public void InstantiateTile(CampaignTilesScript newTilesScript)
    {
        remainingDuration = activeDuration;
        tilesScript = newTilesScript;
        tilesScript.MoveObject(gameObject);
    }

    public void OnChangeGameState(BattleSystem.State newState, BattleSystem.State prevState)
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
        tilesScript.RemoveObject(gameObject);
        gameObject.SetActive(false);
    }
}