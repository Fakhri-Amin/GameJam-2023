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
        EventManager.onChangeGameStateEvent += OnChangeGameState;
        InstantiateLevel(0);
    }

    private void OnDestroy()
    {
        EventManager.onChangeGameStateEvent -= OnChangeGameState;
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

    public void OnChangeGameState(BattleSystem.State newState)
    {
        if (newState == BattleSystem.State.PlayerTurn)
        {
            if (queueUnitSpawn.Count <= 0) return;
            int randomValue = UnityEngine.Random.Range(0, queueUnitSpawn.Count);
            UnitBattleHandler.Instance.AddNewUnit(queueUnitSpawn[randomValue].unitID);
            queueUnitSpawn[randomValue].count--;
            if (queueUnitSpawn[randomValue].count <= 0) queueUnitSpawn.Remove(queueUnitSpawn[randomValue]);
        }
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