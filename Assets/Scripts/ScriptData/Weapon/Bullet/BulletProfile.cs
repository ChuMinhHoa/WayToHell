using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BulletType { 
    pistolBullet
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
    public float bulletDamage;
    public float bulletSpeed;
    public float persentScrits;
    public float knockBackFloat;
    public void InitData(BulletData bulletData) {
        bulletName = bulletData.bulletName;
        bulletType = bulletData.bulletType;
        bulletDamage = bulletData.bulletDamage;
        bulletSpeed = bulletData.bulletSpeed;
        knockBackFloat = bulletData.knockBackFloat;
    }
    public float GetDamaged() {
        int randomCrit = Random.Range(0, 100);
        float resultDamage = bulletDamage;
        if (randomCrit > persentScrits)
            resultDamage *= 2;
        return resultDamage;
    }
}
