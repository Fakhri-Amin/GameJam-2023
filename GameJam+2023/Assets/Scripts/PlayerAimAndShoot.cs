using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimAndShoot : MonoBehaviour
{
    [Header("Gun Stats")]
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Stats")]
    [SerializeField] private int bulletCount;
    [SerializeField] private float timeBetweenShot = 0.08f;

    [Header("Projectile Line")]
    [SerializeField] private LayerMask layermask;
    [SerializeField] private Vector2 minMaxAngle;
    [SerializeField] private LineRenderer lineRenderer;

    private Vector2 gunPosition;
    private Vector2 direction;
    private RaycastHit2D ray;
    private float angle;
    private DotsProjectileLine dotsProjectileLine;

    private void Awake()
    {
        dotsProjectileLine = GetComponent<DotsProjectileLine>();
    }

    private void Start()
    {
        BattleSystem.Instance.OnPlayerTurn += BattleSystem_OnPlayerTurn;
    }

    private void BattleSystem_OnPlayerTurn(Action onActionComplete)
    {
        HandleRotation();

        // Handle Shooting
        if (GameInput.Instance.IsOnMouseLeftUp())
        {
            onActionComplete();
            StartCoroutine(Shoot());
        }
    }

    private void HandleRotation()
    {
        if (GameInput.Instance.IsOnMouseLeftBeingPressed())
        {
            lineRenderer.enabled = true;

            ray = Physics2D.Raycast(bulletSpawnPoint.position, transform.right, 30f, layermask);

            Vector2 reflectPosition = Vector2.Reflect(new Vector3(ray.point.x, ray.point.y) - transform.position, ray.normal);
            Vector2 mousePosition = GameInput.Instance.GetMousePosition();
            gunPosition = Camera.main.WorldToScreenPoint(transform.position);
            direction = mousePosition - gunPosition;

            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (angle >= minMaxAngle.x && angle <= minMaxAngle.y)
            {
                // lineRenderer.SetPosition(0, bulletSpawnPoint.position);
                // lineRenderer.SetPosition(1, ray.point);
                // lineRenderer.SetPosition(2, ray.point + reflectPosition.normalized * 2f);

                dotsProjectileLine.DrawDottedLine(transform.position, ray.point);
                dotsProjectileLine.DrawDottedLine(ray.point, ray.point + reflectPosition.normalized * 2f);
            }

            transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private IEnumerator Shoot()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            // Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
            GameObject bullet = ObjectPool.Instance.GetPooledObject();

            if (bullet != null)
            {
                bullet.transform.position = bulletSpawnPoint.position;
                bullet.SetActive(true);
            }

            yield return new WaitForSeconds(timeBetweenShot);
        }
    }

    public int GetBulletCount()
    {
        return bulletCount;
    }
}
