using UnityEngine;

[CreateAssetMenu(menuName = "PlayerSkill/ActiveSkill/FocusTorrentSkill", fileName = "FocusTorrentSkillSO")]
public class FocusTorrentSkillSO : ActiveSkillSO
{
    public AreaAttackScript waterAttackPrefab;
    public float baseDamage;
    public float levelDamage;

    public override void OnSkillUse(PlayerManager player, ActiveSkillScript activeSkill)
    {
        base.OnSkillUse(player, activeSkill);
        var attack = Instantiate(waterAttackPrefab, player.attackParent).GetComponent<AreaAttackScript>();
        attack.InstantiateAttack(activeSkill);
    }

    public override void OnSkillHit(ActiveSkillScript activeSkill, UnitBase unitHit)
    {
        base.OnSkillHit(activeSkill, unitHit);
        var newDamage = new Damage() { damageValue = GetDamage(activeSkill) };
        unitHit.Damage(newDamage);
        SoundManager.Instance.PlayBulletCollideSound();
    }

    float GetDamage(ActiveSkillScript activeSkill)
    {
        return baseDamage + (levelDamage * activeSkill.currentSkillLevel);
    }
}