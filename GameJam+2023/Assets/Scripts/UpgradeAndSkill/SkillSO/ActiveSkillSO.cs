using UnityEngine;

public abstract class ActiveSkillSO : BaseSkillSO
{
    public Sprite skillSprite;
    public int cooldownDuration;
    public float manaCost;

    public override SkillScript GetSkillScript()
    {
        ActiveSkillScript newActiveSkill = new ActiveSkillScript() { baseSkillSO = this, currentSkillLevel = 1 };
        return newActiveSkill;
    }

    public virtual void OnSkillUse(PlayerManager player, ActiveSkillScript activeSkill)
    {

    }

    public virtual void OnSkillHit(ActiveSkillScript activeSkill, UnitBase unitHit)
    {

    }

}