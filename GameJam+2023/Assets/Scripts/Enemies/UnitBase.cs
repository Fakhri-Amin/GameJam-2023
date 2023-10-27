using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour, IDamageable, IMove, IAttack
{
    [field: SerializeField] public float MaxHealth { get; set; } = 10f;
    [field: SerializeField] public float CrashDamage { get; set; } = 10f;
    public event Action OnHealthChanged;


    public float CurrentHealth { get; set; }
    public float TimeToMove { get; set; }
    public Vector2 CurrentPosition { get; set; }
    public Vector2 TargetPosition { get; set; }

    private void Start()
    {
        CurrentHealth = MaxHealth;
        OnHealthChanged?.Invoke();
    }

    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

        OnHealthChanged?.Invoke();

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
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

    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }

    public Vector2 GetUnitPosition()
    {
        return transform.position;
    }
}
