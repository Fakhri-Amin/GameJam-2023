using UnityEngine;

 public class SpareAmmoSkillSO : PassiveSkillSO
{
    [SerializeField]
    int extraBullet = 2;

    public override void OnSkillAdded()
    {
        base.OnSkillAdded();

    }

}