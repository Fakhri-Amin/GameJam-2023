using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    /*public HealSkillScript healSkill;
    public ExplosionScript explosionSkill;*/
    [HideInInspector]
    public PlayerManager playerManager { private get; set; }
    List<SkillScript> skillList;
    bool isPlayerTurn;

    private void Awake()
    {
        skillList = new List<SkillScript>();
        /*healSkill.skillID = skillList.Count;
        skillList.Add(healSkill);
        explosionSkill.skillID = skillList.Count;
        skillList.Add(explosionSkill);*/
    }

    private void Start()
    {
        EventManager.onChangeGameStateEvent += OnChangeGameState;
        EventManager.onLevelStartEvent += OnLevelStart;
        /*foreach (var skill in skillList)
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
        }*/
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
                //(skillList[GameInput.Instance.CurrentSelectedSkill()] as AimSkill).UseSkill(CampaignManager.instance.GetCurrentCampaign().tilesManager.GetNearestTile(mousePosition));
            }
        }
    }

    public void AddSkill(PlayerManager player, SkillScript newSkill)
    {
        foreach (var skill in skillList)
        {
            if (skill.baseSkillSO == newSkill.baseSkillSO)
            {
                skill.SkillUpgrade(player);
                return;
            }
        }
        skillList.Add(newSkill);
        newSkill.StartSkill(playerManager);
    }

    public void RemoveSkill(PlayerManager player, BaseSkillSO SkillSO)
    {
        SkillScript removedSkill = null;
        foreach (var skill in skillList)
        {
            if (skill.baseSkillSO == SkillSO)
            {
                skill.SkillDowngrade(player);
                removedSkill = skill;
                break;
            }
        }
        if (removedSkill != null && removedSkill.currentSkillLevel < 1)
        {
            removedSkill.EndSkill(playerManager);
            skillList.Remove(removedSkill);
        }
    }


    public void OnLevelStart(int levelID)
    {
        foreach (var skill in skillList)
        {
           
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

public class SkillScript
{
    public BaseSkillSO baseSkillSO;
    public int currentSkillLevel = 1;

    public virtual void StartSkill(PlayerManager player)
    {
        Debug.Log("StartSkill" + baseSkillSO.name);
        baseSkillSO.OnSkillStart(player);
        baseSkillSO.OnSkillUpdateLevel(player, currentSkillLevel);
    }

    public virtual void SkillUpgrade(PlayerManager player)
    {
        Debug.Log("SkillUpgrade" + baseSkillSO.name);
        currentSkillLevel++;
        baseSkillSO.OnSkillUpdateLevel(player, currentSkillLevel);
    }

    public virtual void SkillDowngrade(PlayerManager player)
    {
        Debug.Log("SkillDowngrade" + baseSkillSO.name);
        currentSkillLevel--;
        baseSkillSO.OnSkillUpdateLevel(player, currentSkillLevel);
    }

    public virtual void TurnPass()
    {

    }

    public virtual void EndSkill(PlayerManager player)
    {
        Debug.Log("EndSkill" + baseSkillSO.name);
        baseSkillSO.OnSkillEnd(player);
    }

}

public class PassiveSkillScript : SkillScript
{
    public virtual PassiveSkillSO GetPassiveSkillSO()
    {
        return (baseSkillSO as PassiveSkillSO);
    }
}

public class ActiveSkillScript : SkillScript
{
    public int cooldownDuration;
    public float manaCost;

    int cooldownRemaining;
    PlayerSkillButton skillButton;

    public override void StartSkill(PlayerManager player)
    {
        base.StartSkill(player);
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

    public override void TurnPass()
    {
        UpdateCooldown(cooldownRemaining + 1);
    }

    public void UpdateCooldown(int newValue)
    {
        cooldownRemaining = newValue;
        if (skillButton) skillButton.UpdateCooldown(cooldownRemaining / cooldownDuration);
    }

}

/*public class AimSkill : ActiveSkill
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
public class HealSkillScript : ActiveSkill
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
}*/