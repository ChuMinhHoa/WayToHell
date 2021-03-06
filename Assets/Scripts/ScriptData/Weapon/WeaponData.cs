using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon Data", menuName = "ScriptableObjects/New Weapon Data")]
public class WeaponData : ScriptableObject
{
    public List<SAOWeaponData> listWeaponDatas;
    public SAOWeaponData GetWeaponData(WeaponType weaponType)
    {
        for (int i = 0; i < listWeaponDatas.Count; i++)
        {
            if (listWeaponDatas[i].weaponType == weaponType)
                return listWeaponDatas[i];
        }
        return null;
    }
}
[System.Serializable]
public class SAOWeaponData
{
    public string weaponName;
    public WeaponType weaponType;
    public float weaponDamage;
    public Sprite weaponIcon;
    public Sprite weaponSpite;
    public float countDownAttackTime;
    public float persentScrits;
    public GameObject weaponObject;

    public void InitData(SAOWeaponData weaponData) {
        weaponName = weaponData.weaponName;
        weaponType = weaponData.weaponType;
        weaponDamage = weaponData.weaponDamage;
        weaponIcon = weaponData.weaponIcon;
        weaponSpite = weaponData.weaponSpite;
        countDownAttackTime = weaponData.countDownAttackTime;
    }
    public float GetDamaged()
    {
        int randomCrit = Random.Range(0, 100);
        float resultDamage = weaponDamage;
        if (randomCrit > persentScrits)
            resultDamage *= 2;
        return resultDamage;
    }
}
