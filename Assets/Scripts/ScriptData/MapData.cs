using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MapName { 
    Saharaz,
    Ionia,
    Shurima,
    Targon
}
[System.Serializable]
public class MapData {
    public MapName mapName;
    public List<LevelData> levelDatas;
    public Vector3 mapPos;
    public int levelMapCurrent;
}
