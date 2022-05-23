using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyProfileData
{
    public List<EnemyData> enemyDatas;
    public EnemyData GetEnemyData(EnemyType enemyType) {
        for (int i = 0; i < enemyDatas.Count; i++)
        {
            if (enemyDatas[i].enemyType == enemyType)
                return enemyDatas[i];
        }
        return null;
    }
}
