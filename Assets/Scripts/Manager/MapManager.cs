using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MapManager
{
    public MapProfile mapProfile;
    public List<MapObject> mapObjects;
    MapData mapData;
    public void InitData() {
        int mapIndex = ProfileManager.instance.playerProfile.mapIndex;
        mapData = mapProfile.mapDatas[mapIndex];
        mapData.levelMapCurrent = ProfileManager.instance.playerProfile.levelInMap;
        mapObjects[mapIndex].mapWrap.SetActive(true);
        GameManager.instance.SetPositionForPlayer(mapObjects[mapIndex].playerSpawn);
        CameraControllerCustom.instance.SetPolygonCamera(mapObjects[mapIndex].mapBounce);
        float maxX = mapObjects[mapIndex].maxX.position.x;
        float minX = mapObjects[mapIndex].minX.position.x;
        float maxY = mapObjects[mapIndex].maxY.position.y;
        float minY = mapObjects[mapIndex].minY.position.y;
        GameManager.instance.spawnManager.SetMaxMinPosSpawn(maxX, maxY, minX, minY);
        GameManager.instance.spawnManager.InitMapData();
    }
    public void Update() {
        if (Input.GetKeyDown(KeyCode.P))
        {
            int mapIndex = 1;
            MapData mapData = mapProfile.mapDatas[mapIndex];
            mapData.levelMapCurrent = ProfileManager.instance.playerProfile.levelInMap;
            CameraControllerCustom.instance.SetPolygonCamera(mapObjects[mapIndex].mapBounce);
            mapObjects[mapIndex].mapWrap.SetActive(true);
            GameManager.instance.SetPositionForPlayer(mapObjects[mapIndex].playerSpawn);
        }
    }
    public void MapLevelUp() {
        ProfileManager.instance.playerProfile.levelInMap += 1;
        ProfileManager.instance.playerProfile.SaveProfile();
    }
    public MapData GetMapdata() {
        return mapData;
    }
}
