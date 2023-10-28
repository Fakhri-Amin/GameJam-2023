using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, IDamageable, IMove, IAttack
{
    [field: SerializeField] public float MaxHealth { get; set; } = 10f;
    [field: SerializeField] public float CrashDamage { get; set; } = 10f;
    [field: SerializeField] public int xSize { get; set; } = 1;
    [field: SerializeField] public int ySize { get; set; } = 1;
    public event Action OnHealthChanged;


    public float CurrentHealth { get; set; }
    public float TimeToMove { get; set; }
    public Vector2 CurrentPosition { get; set; }
    public Vector2 TargetPosition { get; set; }
    List<BuffScript> currentAppliedBuff = new List<BuffScript>();
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
        PassiveSkill();
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
        EnemySpawnManager.instance.RemoveUnit(this);
        UnitBattleHandler.Instance.RemoveUnitFromUnitList(this);
        Destroy(gameObject);
    }

    public IEnumerator Move(Vector2 direction)
    {
        EnemySpawnManager.instance.RemoveUnit(this);
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
        EnemySpawnManager.instance.MoveUnit(this);
    }

    public virtual void Attack()
    {

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
        }
    }
}

public class BuffScript
{
    public string buffID;
    public bool isStackable;
    public int value;

    public virtual bool IsExpired()
    {
        return value <= 0;
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