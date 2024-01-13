using UnityEngine;

public class UpgradePartScript : MonoBehaviour
{
    public UpgradePartEnum partEnum;
    public BaseSkillSO SkillPart;

    public void InstantiatePart(PlayerSkillManager skillManager)
    {

    }

    public void RemovePart(PlayerSkillManager skillManager)
    {

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