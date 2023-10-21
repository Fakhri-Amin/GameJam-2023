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
        BattleSystem.Instance.OnStateChanged += BattleSystem_OnStateChanged;
    }

    private void BattleSystem_OnStateChanged(Action onCompleteAction)
    {
        if (BattleSystem.Instance.IsWaiting())
        {
            if (bulletCount == player.GetBulletCount())
            {
                onCompleteAction();
                bulletCount = 0;
            }
        }
    }

    public void IncrementBulletCount()
    {
        bulletCount++;
    }
}
