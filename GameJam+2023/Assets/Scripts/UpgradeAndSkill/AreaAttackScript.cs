using System;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttackScript : MonoBehaviour
{
    public Collider2D collider;
    public float attackAnimationDuration;
    public float attackTime;
    float activeTimer;
    bool hitTime;
    List<UnitBase> currentInsideUnit = new List<UnitBase>();

    ActiveSkillScript currentActiveSkill;

    private void Awake()
    {
        activeTimer = 0;
        EventManager.instance.DisableInput(true);
    }

    private void OnDestroy()
    {
        EventManager.instance.DisableInput(false);
    }

    private void Update()
    {
        activeTimer += Time.deltaTime;
        if (!hitTime && activeTimer >= attackTime)
        {
            hitTime = true;
            for (var i = currentInsideUnit.Count - 1; i >= 0; i--)
            {
                currentActiveSkill.GetActiveSkillSO().OnSkillHit(currentActiveSkill, currentInsideUnit[i]);
            }
        }
        if (activeTimer >= attackAnimationDuration)
        {
            EndAttack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<UnitBase>(out UnitBase unitHit))
        {
            currentInsideUnit.Add(unitHit);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<UnitBase>(out UnitBase unitHit))
        {
            currentInsideUnit.Remove(unitHit);
        }
    }

    public void InstantiateAttack(ActiveSkillScript newActiveSkill)
    {
        currentActiveSkill = newActiveSkill;
    }

    public void EndAttack()
    {
        CampaignManager.instance.GetCurrentCampaign().AfterSkillAttack();
        Destroy(gameObject);
    }

}