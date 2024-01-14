using UnityEngine;

public class UpgradePartScript : MonoBehaviour
{
    public UpgradePartEnum partEnum;
    public BaseSkillSO SkillPart;
    [SerializeField]
    string upgradeName;
    public string UpgradeName
    {
        get { return (string.IsNullOrEmpty(upgradeName) ? name : upgradeName); }
        set { }
    }
    public Sprite upgradeSprite;
    [SerializeField]
    string upgradeDescription;
    public string UpgradeDescription
    {
        get { return (string.IsNullOrEmpty(upgradeDescription) ? UpgradeName : upgradeDescription); }
        set { }
    }

    public void InstantiatePart(PlayerManager player)
    {
        if (SkillPart != null)
        {
            player.skillManager.AddSkill(player, SkillPart.GetSkillScript());
        }
    }

    public void RemovePart(PlayerManager player)
    {
        if (SkillPart != null)
        {
            player.skillManager.RemoveSkill(player, SkillPart);
        }
    }

}

public enum UpgradePartEnum
{
    LeftArm,
    RightArm,
    LeftShoulder,
    RightShoulder,
    Back,
    Foot,
    Body
}