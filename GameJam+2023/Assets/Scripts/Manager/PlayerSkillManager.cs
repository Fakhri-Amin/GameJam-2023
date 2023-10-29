using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public HealSkillScript healSkill;
    public ExplosionScript explosionSkill;
    List<SkillScript> skillList;

    private void Awake()
    {
        skillList = new List<SkillScript>();
        skillList.Add(healSkill);
        skillList.Add(explosionSkill);
    }

    private void Start()
    {
        EventManager.onChangeGameStateEvent += OnChangeGameState;
        EventManager.onLevelStartEvent += OnLevelStart;
        foreach (var skill in skillList)
        {
            foreach (var skillButton in UIManager.instance.playerSkills.playerSkills)
            {
                if (skillButton.skillID == skill.skillID)
                {
                    skill.SetButton(skillButton);
                    skillButton.InstantiateButton(skill);
                    break;
                }
            }
        }
    }

    private void OnDestroy()
    {
        EventManager.onChangeGameStateEvent -= OnChangeGameState;
    }

    public void OnLevelStart(int levelID)
    {
        foreach (var skill in skillList)
        {
            skill.Instantiate();
        }
    }

    public void OnChangeGameState(BattleSystem.State newState)
    {
        if (newState == BattleSystem.State.PlayerTurn)
        {
            foreach (var skill in skillList)
            {
                skill.TurnPass();
            }
        }
    }


}

[System.Serializable]
public class SkillScript
{
    public string skillID;
    public int cooldownDuration;

    int cooldownRemaining;
    PlayerSkillButton skillButton;

    public virtual void Instantiate()
    {
        UpdateCooldown(cooldownDuration);
    }

    public void SetButton(PlayerSkillButton newButton)
    {
        skillButton = newButton;
    }

    public virtual void OnSkillSelect()
    {

    }

    public virtual void OnSkillUse()
    {
        UpdateCooldown(0);
    }

    public void TurnPass()
    {
        UpdateCooldown(cooldownRemaining + 1);
    }

    public void UpdateCooldown(int newValue)
    {
        cooldownRemaining = newValue;
        skillButton.UpdateCooldown(cooldownRemaining / cooldownDuration);
    }

}

[System.Serializable]
public class HealSkillScript : SkillScript
{
    public int healValue;

    public override void OnSkillSelect()
    {
        base.OnSkillSelect();
        OnSkillUse();
    }

    public override void OnSkillUse()
    {
        Heal newHeal = new Heal() { healValue = this.healValue };
        EventManager.instance.OnHealPlayer(newHeal);
        base.OnSkillUse();
    }
}

[System.Serializable]
public class ExplosionScript : SkillScript
{
    public int damageValue;
    public int xSize;
    public int ySize;

    public override void OnSkillSelect()
    {
        base.OnSkillSelect();
    }

    public override void OnSkillUse()
    {
        base.OnSkillUse();
    }
}