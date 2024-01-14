using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletDestroyCollider : MonoBehaviour
{
    private int bulletEnterCount;
    private bool isAnyBulletLeft;

    private void Start()
    {
        BattleSystem.Instance.OnWaiting += BattleSystem_OnWaiting;
    }

    private void BattleSystem_OnWaiting(Action onCompleteAction)
    {
        if (isAnyBulletLeft == true)
        {
            onCompleteAction();
            isAnyBulletLeft = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<BulletBehavior>(out BulletBehavior bulletBehavior))
        {
            bulletEnterCount++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<BulletBehavior>(out BulletBehavior bulletBehavior))
        {
            other.gameObject.SetActive(false);
            bulletEnterCount--;
            if (bulletEnterCount == 0)
            {
                isAnyBulletLeft = true;
            }
        }
    }
}
