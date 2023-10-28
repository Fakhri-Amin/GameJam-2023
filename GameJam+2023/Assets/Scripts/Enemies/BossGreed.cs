using UnityEngine;

public class BossGreed : UnitBase
{
    public int ShieldValue; 

    
}

public class ShieldBuff : BuffScript
{
    public override Damage OnUnitReceiveDamage(Damage damage)
    {
        if (damage.damageValue > 0 && value > 0)
        {
            damage.damageValue = 0;
            value -= 1;
        }
       
        return damage;
    }
}