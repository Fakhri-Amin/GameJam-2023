using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    float CurrentHealth { get; set; }
    float MaxHealth { get; set; }
    void Damage(Damage damage);
    void Die();
}

public class Damage
{
    public float damageValue;
}

public class Heal
{
    public float healValue;
}