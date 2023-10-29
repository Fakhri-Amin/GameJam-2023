using UnityEngine;

public class BossGreed : UnitBase
{
    [Header("Shield Parameter")]
    public string shieldID;
    public int shieldValue;

    public override void UnitStart()
    {
        ApplyBuff(GetShieldBuff());
        base.UnitStart();
    }

    public override void AfterUnitTurn()
    {
        ApplyBuff(GetShieldBuff());
        base.AfterUnitTurn();
    }

    ShieldBuff GetShieldBuff()
    {
        ShieldBuff newShieldBuff = new ShieldBuff()
        {
            buffID = shieldID,
            unit = this,
            isStackable = true,
            maxValue = shieldValue,
            value = shieldValue,
            maxDuration = int.MaxValue,
            duration = 100
        };
        return newShieldBuff;
    }
}

public class ShieldBuff : BuffScript
{
    public override Damage OnUnitReceiveDamage(Damage damage)
    {
        if (damage.damageValue > 0 && value > 0)
        {
            damage.damageValue = 0;
            UpdateBuffValue(value - 1);
        }
        return damage;
    }

}