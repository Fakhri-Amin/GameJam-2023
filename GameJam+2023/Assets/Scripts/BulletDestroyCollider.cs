using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletDestroyCollider : MonoBehaviour
{
    [SerializeField] private PlayerAimAndShoot player;

    private int bulletCount;

    private void Start()
    {
        BattleSystem.Instance.OnWaiting += BattleSystem_OnWaiting;
    }

    private void BattleSystem_OnWaiting(Action onCompleteAction)
    {
        if (bulletCount == player.GetBulletCount())
        {
            onCompleteAction();
            bulletCount = 0;
        }
    }

    public void IncrementBulletCount()
    {
        bulletCount++;
    }
}
