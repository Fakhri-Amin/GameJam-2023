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
        Debug.Log("OnSkillUpdateLevel" + player.shootController.ExtraBulletCount);
        player.shootController.ExtraBulletCount = extraBullet * newLevel;
        Debug.Log("OnSkillUpdateLevel" + player.shootController.ExtraBulletCount);
    }

    public override void OnSkillEnd(PlayerManager player)
    {
        base.OnSkillEnd(player);
    }

}