using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    PlayerController moveController;
    PlayerAimAndShoot shootController;
    PlayerScript playerStats;
    [SerializeField]
    PlayerUpgradeManager upgradeManager;
    [SerializeField]
    PlayerSkillManager skillManager;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            moveController = GetComponent<PlayerController>();
            shootController = GetComponent<PlayerAimAndShoot>();
            playerStats = GetComponent<PlayerScript>();
        }
    }

    public void AddUpgradePart(UpgradePartScript newUpgrade)
    {
        upgradeManager.AddUpgradePart(newUpgrade, skillManager);
    }



}
