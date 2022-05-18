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
        anim.SetTrigger("shot");
        StopAllCoroutines();
        StartCoroutine(GunShot(aimAngle));
    }
    public virtual IEnumerator GunShot(float aimAngle) {
        yield return new WaitForSeconds(animAttackTime);
        Debug.Log("gun Shot");
        GameObject bulletCreated = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        bulletCreated.transform.eulerAngles = new Vector3(0, 0, aimAngle);
        bulletCreated.GetComponent<BulletBase>().weaponData = weaponData;
        anim.ResetTrigger("shot");
    }
}

