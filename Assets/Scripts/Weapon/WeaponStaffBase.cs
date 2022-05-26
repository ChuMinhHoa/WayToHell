using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStaffBase : WeaponGunBase
{
    public override IEnumerator GunShot(float aimAngle) {
        yield return new WaitForSeconds(animAttackTime);
        GameObject bulletCreated = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        Transform targetTransForm = GameObject.FindGameObjectWithTag("Player").transform;
        BulletHomming bulletHome = bulletCreated.GetComponent<BulletHomming>();
        bulletHome.Settarget(targetTransForm.position);
        bulletHome.weaponData = weaponData;
        anim.ResetTrigger("shot");
    }
}
