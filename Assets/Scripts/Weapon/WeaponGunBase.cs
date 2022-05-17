using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGunBase : WeaponBase
{
    public GameObject bullet;
    public Transform shotPoint;
    public override void WeaponAttack(float aimAngle)
    {
        base.WeaponAttack(aimAngle);
        GunShot(aimAngle);
    }
    public virtual void GunShot(float aimAngle) {
        Debug.Log("gun Shot");
        GameObject bulletCreated = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        bulletCreated.transform.eulerAngles = new Vector3(0, 0, aimAngle);
    }
}

