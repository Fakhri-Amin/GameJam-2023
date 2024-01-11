using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public HealSkillScript healSkill;
    public ExplosionScript explosionSkill;
    List<SkillScript> skillList;
    bool isPlayerTurn;

    private void Awake()
    {
        skillList = new List<SkillScript>();
        healSkill.skillID = skillList.Count;
        skillList.Add(healSkill);
        explosionSkill.skillID = skillList.Count;
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
                if (skillButton.skillID == skill.skillName)
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

    private void Update()
    {
        if (isPlayerTurn)
        {
            if (GameInput.Instance.IsOnMouseLeftUpOutsideGameplay() && !GameInput.Instance.IsBasicAttack())
            {
                Vector2 mousePosition = GameInput.Instance.GetMousePosition();
                (skillList[GameInput.Instance.CurrentSelectedSkill()] as AimSkill).UseSkill(CampaignManager.instance.GetCurrentCampaign().tilesManager.GetNearestTile(mousePosition));
            }
        }
    }


    public void OnLevelStart(int levelID)
    {
        foreach (var skill in skillList)
        {
            skill.Instantiate();
        }
    }

    public void OnChangeGameState(BattleSystem.State newState, BattleSystem.State prevState)
    {
        if (newState == BattleSystem.State.PlayerTurn)
        {
            foreach (var skill in skillList)
            {
                skill.TurnPass();
            }
        }
        isPlayerTurn = newState == BattleSystem.State.PlayerTurn;
    }
}

[System.Serializable]
public class SkillScript
{
    public string skillName;
    public int skillID;
    public int cooldownDuration;
    public float manaCost;

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
        if (skillButton) skillButton.UpdateCooldown(cooldownRemaining / cooldownDuration);
    }

}

public class AimSkill : SkillScript
{
    public override void OnSkillSelect()
    {
        base.OnSkillSelect();

    }

    public virtual void UseSkill(TileTransform tile )
    {

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
public class ExplosionScript : AimSkill
{
    public int damageValue;
    public int xSize;
    public int ySize;

    public override void OnSkillSelect()
    {
        GameInput.Instance.EquipSkill(skillID);
        base.OnSkillSelect();
    }

    public override void UseSkill(TileTransform tile)
    {
        for (var i = tile.GetXLeft(); i <= tile.GetXRight(); i++)
        {
            for (var j = tile.GetYDown(); j <= tile.GetYUp(); j++)
            {
                if (tile.unitOnTile != null)
                {
                    var newDamage = new Damage() { damageValue = damageValue };
                    tile.unitOnTile.Damage(newDamage);
                }
            }
        }
        base.UseSkill(tile);
        OnSkillUse();
    }
}