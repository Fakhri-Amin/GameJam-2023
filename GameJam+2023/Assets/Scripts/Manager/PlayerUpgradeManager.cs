using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeManager : MonoBehaviour
{
    public List<UpgradePartScript> currentUpgradeList;
    public Transform upgradePartParent;

    public void AddUpgradePart(UpgradePartScript newUpgrade, PlayerSkillManager skillManager)
    {
        bool foundSamePart = false;
        UpgradePartScript sameSlotPart = null;
        foreach (var currentPart in currentUpgradeList)
        {
            if (currentPart.name == newUpgrade.name)
            {
                foundSamePart = true;
                break;
            }
            else if (currentPart.partEnum == newUpgrade.partEnum)
            {
                sameSlotPart = currentPart;
            }
        }
        if (!foundSamePart)
        {
            if (sameSlotPart != null)
            {
                sameSlotPart.RemovePart(skillManager);
                Destroy(sameSlotPart);
            }
            var newPart = Instantiate(newUpgrade.gameObject, upgradePartParent).GetComponent<UpgradePartScript>();
            newPart.InstantiatePart(skillManager);
            currentUpgradeList.Add(newPart);
        }
    }

}