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
    private List<Vector2> unitPositionList = new();

    private void Awake()
    {
        Instance = this;

        unitList = FindObjectsOfType<UnitBase>().ToList();
        
    }

    private void Start()
    {
        foreach (UnitBase unitBase in unitList)
        {
            unitPositionList.Add(unitBase.GetUnitPosition());
            EnemySpawnManager.instance.MoveUnit(unitBase);
        }
        BattleSystem.Instance.OnUnitTurn += BattleSystem_OnUnitTurn;
    }

    private void BattleSystem_OnUnitTurn(Action onActionComplete)
    {
        foreach (UnitBase unitBase in unitList)
        {
            unitPositionList.Remove(unitBase.GetUnitPosition());
            StartCoroutine(unitBase.Move(Vector2.left));
            unitBase.Attack();
            unitPositionList.Add(unitBase.GetUnitPosition());
        }
        onActionComplete();
    }

    public void RemoveUnitFromUnitList(UnitBase unitBase)
    {
        unitList.Remove(unitBase);
        unitPositionList.Remove(unitBase.GetUnitPosition());
        EnemySpawnManager.instance.RemoveUnit(unitBase);
    }

    public void AddNewUnit()
    {
        var newUnit = EnemySpawnManager.instance.SpawnNewEnemy();
        unitList.Add(newUnit);
        unitPositionList.Add(newUnit.GetUnitPosition());
    }
}
