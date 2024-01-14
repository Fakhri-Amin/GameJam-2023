using UnityEngine;

public abstract class BaseSkillSO : ScriptableObject
{
    public string SkillID;

    public virtual void OnSkillStart(PlayerManager player)
    {

    }

    public virtual void OnSkillEnd(PlayerManager player)
    {

    }

    public virtual void OnSkillUpdateLevel(PlayerManager player, int newLevel)
    {

    }

    public virtual SkillScript GetSkillScript()
    {
        return null;
    }
}