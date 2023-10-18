using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimAndShoot : MonoBehaviour
{
    [Header("Gun Stats")]
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Stats")]
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private int bulletCount;
    [SerializeField] private float timeBetweenShot = 0.08f;

    [Header("Projectile Line")]
    [SerializeField] private LayerMask layermask;
    [SerializeField] private Vector2 minMaxAngle;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Projectile Line : Dots Setting")]
    [SerializeField] private Sprite dotSprite;
    [Range(0.01f, 1f)]
    [SerializeField] private float dotSize;
    [Range(0.01f, 2f)]
    [SerializeField] private float dotDelta;
    [Range(0f, 1f)]
    [SerializeField] private float dotAlpha;


    private Vector2 gunPosition;
    private Vector2 direction;
    private RaycastHit2D ray;
    private float angle;
    private List<Vector2> dotPositionList = new List<Vector2>();
    private List<GameObject> dotGameObjectList = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        HandleShooting();
        HandleDotsLine();
    }

    private void FixedUpdate()
    {

    }

    private void HandleRotation()
    {
        if (GameInput.Instance.IsOnMouseLeftBeingPressed())
        {
            // Rotate using line renderer

            lineRenderer.enabled = true;

            ray = Physics2D.Raycast(bulletSpawnPoint.position, transform.right, 30f, layermask);

            Vector2 reflectPosition = Vector2.Reflect(new Vector3(ray.point.x, ray.point.y) - transform.position, ray.normal);
            Vector2 mousePosition = GameInput.Instance.GetMousePosition();
            gunPosition = Camera.main.WorldToScreenPoint(transform.position);
            direction = mousePosition - gunPosition;

            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (angle >= minMaxAngle.x && angle <= minMaxAngle.y)
            {
                lineRenderer.SetPosition(0, bulletSpawnPoint.position);
                lineRenderer.SetPosition(1, ray.point);
                lineRenderer.SetPosition(2, ray.point + reflectPosition.normalized * 2f);

                // DrawDottedLine(transform.position, ray.point);
                // DrawDottedLine(ray.point, ray.point + reflectPosition.normalized * 2f);
            }

            transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private void HandleShooting()
    {
        if (GameInput.Instance.IsOnMouseLeftUp())
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            Transform bulletTransform = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
            yield return new WaitForSeconds(timeBetweenShot);
        }
    }

    private void HandleDotsLine()
    {
        if (dotPositionList.Count > 0)
        {
            DestroyAllDots();
            dotPositionList.Clear();
        }
    }

    private void DestroyAllDots()
    {
        foreach (GameObject dot in dotGameObjectList)
        {
            Destroy(dot);
        }
        dotGameObjectList.Clear();
    }

    private GameObject GetOneDot()
    {
        GameObject newGameObject = new();
        newGameObject.transform.localScale = Vector3.one * dotSize;

        newGameObject.transform.parent = transform;

        SpriteRenderer spriteRenderer = newGameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, dotAlpha);
        spriteRenderer.sprite = dotSprite;

        return newGameObject;
    }

    private void DrawDottedLine(Vector2 start, Vector2 end)
    {
        DestroyAllDots();

        Vector2 point = start;
        Vector2 direction = (end - start).normalized;

        while ((end - start).magnitude > (point - start).magnitude)
        {
            dotPositionList.Add(point);
            point += direction * dotDelta;
        }

        RenderDots();
    }

    private void RenderDots()
    {
        foreach (Vector2 position in dotPositionList)
        {
            GameObject dot = GetOneDot();
            dot.transform.position = position;
            dotGameObjectList.Add(dot);
        }
    }
}
