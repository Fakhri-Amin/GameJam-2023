using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UnitBattleHandler : MonoBehaviour
{
    public static UnitBattleHandler Instance;

    private List<UnitBase> unitList = new();

    private void Awake()
    {
        Instance = this;

        unitList = FindObjectsOfType<UnitBase>().ToList();
    }

    private void Start()
    {
        BattleSystem.Instance.OnUnitTurn += BattleSystem_OnUnitTurn;
    }

    private void BattleSystem_OnUnitTurn(Action onActionComplete)
    {
        foreach (UnitBase unitBase in unitList)
        {
            StartCoroutine(unitBase.Move(Vector2.left));
        }
        onActionComplete();
    }

    public void RemoveUnitFromUnitList(UnitBase unitBase)
    {
        unitList.Remove(unitBase);
    }
}
