using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/CampaignScriptableObject", fileName = "CampaignSO")]
public class CampaignSO : ScriptableObject
{
    public List<LevelSO> levelDatas;

}