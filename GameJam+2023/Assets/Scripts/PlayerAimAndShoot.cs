using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimAndShoot : MonoBehaviour
{
    [SerializeField] private Transform gun;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform bulletPrefab;

    private Vector2 worldPosition;
    private Vector2 direction;


    // Update is called once per frame
    void Update()
    {
        HandleGunRotation();
        HandleGunShooting();
    }

    private void HandleGunRotation()
    {
        // Rotate the gun towards the mouse position
        worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = (worldPosition - (Vector2)gun.transform.position).normalized;
        gun.transform.right = direction;
    }

    private void HandleGunShooting()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Spawn the bullet
            Transform bulletTransform = Instantiate(bulletPrefab, bulletSpawnPoint.position, gun.transform.rotation);
        }
    }
}
