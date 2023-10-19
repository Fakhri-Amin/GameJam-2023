using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyCollider : MonoBehaviour
{
    // private EdgeCollider2D edgeCollider2D;

    // private void Awake()
    // {
    //     edgeCollider2D = GetComponent<EdgeCollider2D>();
    // }

    // private void Start()
    // {
    //     BattleSystem.Instance.OnStateChanged += BattleSystem_OnStateChanged;
    //     edgeCollider2D.enabled = false;
    // }

    // private void BattleSystem_OnStateChanged(Action onActionComplete)
    // {
    //     if (BattleSystem.Instance.IsWaiting() && ObjectPool.Instance.IsBulletActiveInHieararchy())
    //     {
    //         edgeCollider2D.enabled = true;
    //     }
    //     else
    //     {
    //         edgeCollider2D.enabled = false;
    //         // onActionComplete();
    //     }
    // }
}
