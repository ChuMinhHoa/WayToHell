using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BulletType { 
    pistolBullet,
    pistolBulletEnemy,
    sandBullet
}
[CreateAssetMenu(fileName = "BulletProFile", menuName = "ScriptableObjects/New Bullet Profile")]
public class BulletProfile : ScriptableObject
{
    public List<BulletData> bulletDatas;
    public BulletData GetBulletData(BulletType bulletType) {
        foreach (BulletData bullet in bulletDatas)
        {
            if (bullet.bulletType == bulletType)
                return bullet;
        }
        return null;
    }
}
[System.Serializable]
public class BulletData {
    public string bulletName;
    public BulletType bulletType;
    public float bulletSpeed;
    public float knockBackFloat;
    public float knockBackTime;
    public void InitData(BulletData bulletData) {
        bulletName = bulletData.bulletName;
        bulletType = bulletData.bulletType;
        bulletSpeed = bulletData.bulletSpeed;
        knockBackFloat = bulletData.knockBackFloat;
        knockBackTime = bulletData.knockBackTime;
    }
    
}
