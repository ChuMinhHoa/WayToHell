using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "New Level Data",menuName = "ScriptableObjects/New Level Data")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public List<WayData> wayDatas;
}
