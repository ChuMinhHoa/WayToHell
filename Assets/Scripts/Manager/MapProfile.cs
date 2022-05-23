using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MapProfileData", menuName = "ScriptableObjects/New Map Profile")]
public class MapProfile : ScriptableObject {
    public List<MapData> mapDatas;
    public MapData GetMapData(MapName mapName) {
        foreach (MapData data in mapDatas)
        {
            if (data.mapName == mapName)
                return data;
        }
        return null;
    }
}
