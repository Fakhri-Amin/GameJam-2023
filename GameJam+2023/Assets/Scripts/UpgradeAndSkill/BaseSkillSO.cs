using UnityEngine;

public abstract class BaseSkillSO : ScriptableObject
{
    public string SkillID;

    public virtual void OnSkillAdded()
    {

    }

    public virtual void OnSkillRemoved()
    {

    }

    public virtual void OnSkillUpdateLevel(int newLevel)
    {

    }
}