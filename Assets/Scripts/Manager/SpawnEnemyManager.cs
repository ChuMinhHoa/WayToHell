using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnEnemyManager:MonoBehaviour {
    private float maxX, maxY;
    private float minX, minY;
    MapData mapData;
    LevelData levelData;
    int currentWay;
    public int levelCount;
    public int enemiesWayCount;
    public void InitMapData() {
        mapData = GameManager.instance.mapManager.GetMapdata();
        levelCount = mapData.levelDatas.Count;
        InitLevelData();
    }
    void InitLevelData() {
        levelData = mapData.levelDatas[mapData.levelMapCurrent];
        currentWay = -1;
        InitWayData();
    }
    void InitWayData() {
        currentWay += 1;
        if (currentWay >= levelData.wayDatas.Count)
        {
            levelCount -= 1;
            if (levelCount == 0)
            {
                Debug.Log("Next Map");
                ProfileManager.instance.playerProfile.mapIndex += 1;
                ProfileManager.instance.playerProfile.SaveProfile();
                GameManager.instance.mapManager.InitData();
                return;
            }
            Debug.Log("Next Level");
            ProfileManager.instance.playerProfile.levelInMap += 1;
            ProfileManager.instance.playerProfile.SaveProfile();
            mapData.levelMapCurrent += 1;
            InitLevelData();
            return;
        }
        Debug.Log("Next Way "+ levelData.wayDatas[currentWay].enemyTypes.Count);
        enemiesWayCount = levelData.wayDatas[currentWay].enemyTypes.Count;
        SpawnEnemy();
    }
    public void MinusEnemiesWayCount() {
        enemiesWayCount -= 1;
        if (enemiesWayCount == 0)
        {
            Debug.Log("NextWay");
            InitWayData();
        }
    }
    #region spawn enemy
    public void SpawnEnemy() {
        for (int i = 0; i < enemiesWayCount; i++)
        {
            EnemyType enemyType = levelData.wayDatas[currentWay].enemyTypes[i];
            GameObject enemyObject = ProfileManager.instance.enemyProfile.GetEnemyData(enemyType).enemyPrefab;
            Instantiate(enemyObject, RandomPositionSpawn(), Quaternion.identity);
        }
    }
    Vector3 RandomPositionSpawn()
    {
        return new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY),0);
    }
    #endregion
    public void SetMaxMinPosSpawn(float maxX, float maxY, float minX, float minY) {
        this.maxX = maxX;
        this.maxY = maxY;
        this.minX = minX;
        this.minY = minY;
    }
}
