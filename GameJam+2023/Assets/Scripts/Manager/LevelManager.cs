using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Level> levelList;

    List<UnitSpawnData> queueUnitSpawn = new List<UnitSpawnData>();
    bool spawnTag;


    private void Start()
    {
        BattleSystem.Instance.OnPlayerTurn += OnPlayerTurn;
        BattleSystem.Instance.OnUnitTurn += OnUnitTurn;
        InstantiateLevel(0);
    }

    private void OnDestroy()
    {
        BattleSystem.Instance.OnPlayerTurn -= OnPlayerTurn;
        BattleSystem.Instance.OnUnitTurn -= OnUnitTurn;
    }

    void InstantiateLevel(int levelID)
    {
        foreach (var level in levelList)
        {
            if (level.levelID == levelID)
            {
                foreach (var unit in level.initializeUnit)
                {
                    for (var i = 0; i < unit.count; i++)
                    {
                        UnitBattleHandler.Instance.AddNewUnit(unit.unitID);
                    }
                }
                queueUnitSpawn = level.reinforcementUnit;
                break;
            }
        }
        spawnTag = false;
    }

    void OnPlayerTurn(Action onActionComplete)
    {
        if (queueUnitSpawn.Count <= 0) return;
        if (spawnTag)
        {
            int randomValue = UnityEngine.Random.Range(0, queueUnitSpawn.Count);
            UnitBattleHandler.Instance.AddNewUnit(queueUnitSpawn[randomValue].unitID);
            queueUnitSpawn[randomValue].count--;
            if (queueUnitSpawn[randomValue].count <= 0) queueUnitSpawn.Remove(queueUnitSpawn[randomValue]);
           
            spawnTag = false;
        }
    }

    void OnUnitTurn(Action onActionComplete)
    {
        spawnTag = true;
    }


}

[System.Serializable]
public struct Level
{
    public int levelID;
    public List<UnitSpawnData> initializeUnit;
    public List<UnitSpawnData> reinforcementUnit;
}

[System.Serializable]
public class UnitSpawnData
{
    public string unitID;
    public int count;
}