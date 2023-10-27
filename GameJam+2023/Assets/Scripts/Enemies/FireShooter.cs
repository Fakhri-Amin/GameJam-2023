using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShooter : UnitBase
{
    [SerializeField] private GameObject bulletPrefab;

    public override void Attack()
    {
        base.Attack();
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
