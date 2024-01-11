using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay/LevelScriptableObject", fileName = "LevelSO")]
public class LevelSO : ScriptableObject
{
    bool isBoss;
    public List<WaveSO> waveDatas;

}
