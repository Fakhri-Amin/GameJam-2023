using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, IDamageable, IMove, IAttack
{
    public string UnitId;
    [field: SerializeField] public float MaxHealth { get; set; } = 10f;
    [field: SerializeField] public float CrashDamage { get; set; } = 10f;
    [field: SerializeField] public int xSize { get; set; } = 1;
    [field: SerializeField] public int ySize { get; set; } = 1;
    public event Action OnHealthChanged;


    public float CurrentHealth { get; set; }
    public float TimeToMove { get; set; }
    public Vector2 CurrentPosition { get; set; }
    public Vector2 TargetPosition { get; set; }
    protected List<BuffScript> currentAppliedBuff = new List<BuffScript>();
    [HideInInspector]
    public Collider2D collider2D;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
        OnHealthChanged?.Invoke();
        UnitStart();
        PassiveSkill();
    }

    public void InstantiateUnit(CampaignTilesScript newTilesScript)
    {

    }

    public virtual void UnitStart()
    {

    }

    public virtual void Damage(Damage damage)
    {
        foreach (var buff in currentAppliedBuff)
        {
            damage = buff.OnUnitReceiveDamage(damage);
        }
        RemoveExpiredBuff();
        CurrentHealth -= damage.damageValue;

        OnHealthChanged?.Invoke();

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        EventManager.instance.OnEnemyUnitDead(this);
        //UnitBattleHandler.Instance.RemoveUnitFromUnitList(this);
        Destroy(gameObject);
    }

    public void StartUnitTurn()
    {
        StartCoroutine(Move(Vector2.left));
        Attack();
    }

    public IEnumerator Move(Vector2 direction)
    {
        BeforeUnitTurn();
        EventManager.instance.OnEnemyUnitStartMove(this);
        float elapsedTime = 0f;
        CurrentPosition = transform.position;
        TargetPosition = direction + CurrentPosition;

        while (elapsedTime < TimeToMove)
        {
            transform.position = Vector3.Lerp(CurrentPosition, TargetPosition, elapsedTime / TimeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = TargetPosition;
        EventManager.instance.OnEnemyUnitEndMove(this);
        AfterUnitTurn();
    }

    public virtual void BeforeUnitTurn()
    {

    }

    public virtual void AfterUnitTurn()
    {

    }

    public virtual void Attack()
    {
        SoundManager.Instance.PlayEnemyCastSkillSound();
    }

    public virtual void PassiveSkill()
    {

    }

    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }

    public Vector2 GetUnitPosition()
    {
        return transform.position;
    }

    protected virtual void ApplyBuff(BuffScript newBuff)
    {
        bool foundSameBuff = false;
        foreach (var buff in currentAppliedBuff)
        {
            if (buff.buffID == newBuff.buffID)
            {
                foundSameBuff = true;
                if (buff.isStackable)
                {
                    buff.value += newBuff.value;
                    if (buff.value > buff.maxValue) buff.value = buff.maxValue;
                    buff.duration = newBuff.duration;
                }
            }
        }
        if (!foundSameBuff)
        {
            currentAppliedBuff.Add(newBuff);
            newBuff.OnBuffStart();
        }
    }

    void RemoveExpiredBuff()
    {
        var tempBuffs = new List<BuffScript>();
        foreach (var buff in currentAppliedBuff)
        {
            if (buff.IsExpired())
            {
                buff.OnBuffEnd();
                tempBuffs.Add(buff);
            }
        }
        foreach (var buff in tempBuffs)
        {
            currentAppliedBuff.Remove(buff);
            buff.OnBuffEnd();
        }
    }
}

public class BuffScript
{
    public UnitBase unit;
    public string buffID;
    public bool isStackable;
    public int maxValue;
    public int value;
    public int maxDuration;
    public int duration;

    public virtual bool IsExpired()
    {
        return value <= 0 || duration <= 0;
    }

    public virtual void OnBuffStart()
    {

    }

    public virtual void OnUnitTurnStart()
    {

    }

    public virtual void OnUnitTurnEnd()
    {

    }

    public virtual Damage OnUnitReceiveDamage(Damage damage)
    {
        return damage;
    }

    public virtual void OnBuffTriggered()
    {

    }

    public virtual void UpdateBuffValue(int newValue)
    {
        value = newValue;
    }

    public virtual void OnBuffEnd()
    {

    }
}