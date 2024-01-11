using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/WaveScriptableObject", fileName = "WaveSO")]
public class WaveSO : ScriptableObject
{
	public List<MonsterSpawnData> spawnDatas;
}

[System.Serializable]
public class MonsterSpawnData
{
	public UnitBase monsterPrefab;
	[SerializeField]
	int spawnlocationX;
	[SerializeField]
	int spawnlocationY;

	public string GetMonsterUnitID()
    {
		return monsterPrefab.UnitId;
    }

	public UnitBase SpawnMonster(CampaignTilesScript tileScript)
    {
		return tileScript.SpawnNewEnemy(GetMonsterUnitID(), spawnlocationX, spawnlocationY);
    }
}
