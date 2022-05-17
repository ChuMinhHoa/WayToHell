using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponState { 
    Idle,
    Attack,
    ColdDown
}
public enum WeaponType { 
    Gun,
    GunEnemy,
    Sword
}
public class WeaponBase : MonoBehaviour
{
    public WeaponState weaponState;
    public WeaponType weaponType;
    public SAOWeaponData weaponData;
    public float aimAngle;
    private float coldDown;
    public virtual void Start() {
        InitData();
    }
    public virtual void FixedUpdate()
    {
        switch (weaponState)
        {
            case WeaponState.Idle:
                WeaponIdle();
                break;
            case WeaponState.Attack:
                if (coldDown <= 0)
                {
                    WeaponAttack(aimAngle);
                    coldDown = weaponData.countDownAttackTime;
                }
                weaponState = WeaponState.ColdDown;
                break;
            case WeaponState.ColdDown:
                if (coldDown > 0)
                    coldDown -= Time.deltaTime;
                else
                    weaponState = WeaponState.Idle;
                break;
            default:
                break;
        }
    }
    public virtual void InitData() {
        weaponData.InitData(WeaponProfileManager.instance.weaponData.GetWeaponData(weaponType));
    }
    public virtual void WeaponIdle() { }
    public virtual void WeaponAttack(float aimAngle) { }
}
