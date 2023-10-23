using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionHandler : MonoBehaviour
{
    [SerializeField] private float damageAmount = 1;

    private BulletBehavior bulletBehavior;
    private bool isFirstHit;

    private void Awake()
    {
        bulletBehavior = GetComponent<BulletBehavior>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.Damage(damageAmount);
        }
    }
}
