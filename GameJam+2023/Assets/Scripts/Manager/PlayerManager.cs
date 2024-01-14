using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    PlayerController moveController;
    [HideInInspector]
    public PlayerAimAndShoot shootController { get; private set; }
    PlayerScript playerStats;
    public PlayerUpgradeManager upgradeManager;
    public PlayerSkillManager skillManager;
    public Transform attackParent;

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
            skillManager.playerManager = this;
        }
    }

    public void AddUpgradePart(UpgradePartScript newUpgrade)
    {
        upgradeManager.AddUpgradePart(newUpgrade, this);
    }


}
