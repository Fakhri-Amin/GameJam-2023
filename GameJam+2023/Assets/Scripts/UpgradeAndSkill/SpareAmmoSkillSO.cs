using UnityEngine;

[CreateAssetMenu(menuName = "PlayerSkill/PassiveSkill/SpareAmmoSkill", fileName = "SpareAmmoSkillSO")]
public class SpareAmmoSkillSO : PassiveSkillSO
{
    [SerializeField]
    int extraBullet = 2;

    public override void OnSkillStart(PlayerManager player)
    {
        base.OnSkillStart(player);
    }

    public override void OnSkillUpdateLevel(PlayerManager player, int newLevel)
    {
        base.OnSkillUpdateLevel(player, newLevel);
        player.shootController.ExtraBulletCount = extraBullet * newLevel;
    }

    public override void OnSkillEnd(PlayerManager player)
    {
        base.OnSkillEnd(player);
    }

}