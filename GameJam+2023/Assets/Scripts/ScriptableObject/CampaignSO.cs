using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/CampaignScriptableObject", fileName = "CampaignSO")]
public class CampaignSO : ScriptableObject
{
    public List<LevelSO> levelDatas;
    public List<UpgradePartScript> availableUpgradePart;
    public List<UpgradePartScript> initialUpgradePart;

    public List<UpgradePartScript> Get3RandomUpgrade(PlayerUpgradeManager upgradeManager)
    {
        List<UpgradePartScript> currentUpgradePart = new List<UpgradePartScript>(availableUpgradePart);
        foreach (var ownedUpgradePart in upgradeManager.currentUpgradeList)
        {
            UpgradePartScript alreadyOwnedPart = null;
            foreach (var currentUpgrade in currentUpgradePart)
            {
                if (ownedUpgradePart.UpgradeName == currentUpgrade.UpgradeName)
                {
                    alreadyOwnedPart = currentUpgrade;
                    break;
                }
            }
            if (alreadyOwnedPart != null)
            {
                currentUpgradePart.Remove(alreadyOwnedPart);
            }
        }
        List<UpgradePartScript> choosenUpgradePart = new List<UpgradePartScript>();
        for (var i = 0; i< 3; i++)
        {
            int randomNum = Random.Range(0, currentUpgradePart.Count);
            choosenUpgradePart.Add(currentUpgradePart[randomNum]);
            currentUpgradePart.RemoveAt(randomNum);
        }
        return choosenUpgradePart;
    }
}