using UnityEngine;

public abstract class PassiveSkillSO : BaseSkillSO
{
    public override SkillScript GetSkillScript()
    {
        PassiveSkillScript newPassiveSkill = new PassiveSkillScript() { baseSkillSO = this, currentSkillLevel = 1 };
        return newPassiveSkill;
    }

}