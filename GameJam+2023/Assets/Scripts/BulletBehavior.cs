using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 15f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SetVelocity();
    }

    private void SetVelocity()
    {
        rb.velocity = transform.right * bulletSpeed;
    }
}
