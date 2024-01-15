using System;
using System.Collections.Generic;
using UnityEngine;

public class CampaignScript : MonoBehaviour
{
    public CampaignSO campaignData;
    public CampaignTilesScript tilesManager;

    List<ObjectPoolConfig> initialPools;
    ActiveLevel activeLevel;
    int currentLevel = -1;
  

    private void Awake()
    {
        EventManager.onEnemyUnitStartMoveEvent += OnEnemyUnitStartMove;
        EventManager.onEnemyUnitEndMoveEvent += OnEnemyUnitEndMove;
        EventManager.onEnemyUnitDeadEvent += OnEnemyUnitDead;
        EventManager.onChangeGameStateEvent += OnChangeGameState;
        EventManager.onLevelFinishEvent += OnLevelFinish;
        BattleSystem.Instance.OnUnitTurn += BattleSystem_OnUnitTurn;
    }

    private void OnDestroy()
    {
        EventManager.onEnemyUnitStartMoveEvent -= OnEnemyUnitStartMove;
        EventManager.onEnemyUnitEndMoveEvent -= OnEnemyUnitEndMove;
        EventManager.onEnemyUnitDeadEvent -= OnEnemyUnitDead;
        EventManager.onChangeGameStateEvent -= OnChangeGameState;
        EventManager.onLevelFinishEvent -= OnLevelFinish;
        BattleSystem.Instance.OnUnitTurn -= BattleSystem_OnUnitTurn;
    }

    public void InstantiateCampaign()
    {
        List<ObjectPoolConfig> initialPools = new List<ObjectPoolConfig>();
        foreach (var level in campaignData.levelDatas)
        {
            foreach (var wave in level.waveDatas)
            {
                List<UnitCount> tempPools = new List<UnitCount>();
                foreach (var monster in wave.spawnDatas)
                {
                    bool foundUnit = false ;
                    foreach (var config in tempPools)
                    {
                        if (config.unitID == monster.GetMonsterUnitID())
                        {
                            config.unitCount++;
                            break;
                        }
                    }
                    if (!foundUnit)
                    {
                        tempPools.Add(new UnitCount() { unitID = monster.GetMonsterUnitID(), prefab = monster.monsterPrefab.gameObject, unitCount = 1 });
                    }
                }
                foreach (var config in tempPools)
                {
                    bool foundUnit = false;
                    foreach (var poolUnit in initialPools)
                    {
                        //Debug.Log("Is same?" + poolUnit.id + "and" + config.unitID);
                        if (poolUnit.id == config.unitID)
                        {
                            foundUnit = true;
                            if (poolUnit.initialCount < config.unitCount)
                            {
                                //Debug.Log("Add initialCount " + config.unitCount);
                                poolUnit.initialCount = config.unitCount;
                            }
                            break;
                        }
                    }
                    if (!foundUnit)
                    {
                        //Debug.Log("Add new " + config.unitID);
                        initialPools.Add(new ObjectPoolConfig() { id = config.unitID, prefab = config.prefab, initialCount = config.unitCount });
                    }
                }
            }
        }
        EnemySpawnPool.instance.UpdatePools(initialPools);
        StartNewLevel(0);
    }

    public void AfterSkillAttack()
    {
        activeLevel.AfterSkillAttack();
    }

    void StartNewLevel(int levelNum)
    {
        currentLevel = levelNum;
        activeLevel = new ActiveLevel();
        activeLevel.InstantiateLevel(this, campaignData.levelDatas[currentLevel]);
        EventManager.instance.OnLevelStart(currentLevel);
    }

    void OnEnemyUnitStartMove(UnitBase unit)
    {
        if (activeLevel != null)
        {
            tilesManager.RemoveUnit(unit);
        }
    }

    void OnEnemyUnitEndMove(UnitBase unit)
    {
        if (activeLevel != null)
        {
            tilesManager.MoveUnit(unit);
        }
    }

    void OnEnemyUnitDead(UnitBase unit)
    {
        if (activeLevel != null)
        {
            tilesManager.RemoveUnit(unit);
            activeLevel.OnUnitDead(unit);
        }
    }

    void OnChangeGameState(BattleSystem.State newState, BattleSystem.State prevState)
    {
        if (activeLevel != null)
        {
            activeLevel.OnChangeGameState(newState, prevState);
        }
    }

    void OnLevelFinish()
    {
        if (campaignData.levelDatas.Count > currentLevel + 1)
        {
            //UIManager.instance.upgradePanel.InstantiateUpgradeChoice(campaignData.Get3RandomUpgrade(PlayerManager.instance.upgradeManager));
            StartNewLevel(currentLevel + 1);
        }
        else
        {
            EventManager.instance.OnCampaignFinish();
        }
       
    }

    void BattleSystem_OnUnitTurn(Action onActionComplete)
    {
        if (activeLevel != null)
        {
            activeLevel.BattleSystem_OnUnitTurn(onActionComplete);
        }
    }

    public class UnitCount
    {
        public string unitID;
        public int unitCount;
        public GameObject prefab;
    }
}

public class ActiveLevel
{
    CampaignScript campaign;
    LevelSO levelData;

    int currentWaveNum;
    List<UnitBase> currentActiveUnit = new List<UnitBase>();

    public void InstantiateLevel(CampaignScript newCampaign, LevelSO newLevelData)
    {
        campaign = newCampaign;
        levelData = newLevelData;
        currentWaveNum = -1;
        startNewWave();
    }

    public void OnChangeGameState(BattleSystem.State newState, BattleSystem.State prevState)
    {
        if (newState == BattleSystem.State.PlayerTurn && prevState == BattleSystem.State.UnitTurn)
        {
            if (IsEmptyUnit())
            {
                UIManager.instance.upgradePanel.InstantiateUpgradeChoice(campaign.campaignData.Get3RandomUpgrade(PlayerManager.instance.upgradeManager));
                startNewWave();
            }
        }
    }

    public void BattleSystem_OnUnitTurn(Action onActionComplete)
    {
        foreach (UnitBase unitBase in currentActiveUnit)
        {
            unitBase.StartUnitTurn();
        }
        onActionComplete();
    }

    public void AfterSkillAttack()
    {
        if (IsEmptyUnit())
        {
            UIManager.instance.upgradePanel.InstantiateUpgradeChoice(campaign.campaignData.Get3RandomUpgrade(PlayerManager.instance.upgradeManager));
            startNewWave();
        }
    }

    public void OnUnitDead(UnitBase unit)
    {
        currentActiveUnit.Remove(unit);
    }

    bool IsEmptyUnit()
    {
        return currentActiveUnit.Count <= 0;
    }

    void startNewWave()
    {
        currentWaveNum++;
        if (currentWaveNum >= levelData.waveDatas.Count)
        {
            EventManager.instance.OnLevelFinish();
        }
        else
        {
            foreach (var unit in levelData.waveDatas[currentWaveNum].spawnDatas)
            {
                currentActiveUnit.Add(unit.SpawnMonster(campaign.tilesManager));
            }
        }
        
    }
    
}
