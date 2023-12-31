using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        SetVelocity();
    }

    public void SetVelocity()
    {
        rb.velocity = transform.right * moveSpeed;
    }

    private void Update()
    {
        rb.velocity = rb.velocity.normalized * moveSpeed;
    }
}
