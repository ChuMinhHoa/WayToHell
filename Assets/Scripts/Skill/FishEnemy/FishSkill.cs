using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSkill : SkillBase {
    public WeaponStaffBase staff;
    public int bulletCount;
    public override void UpdateSkill()
    {
        switch (skillState)
        {
            case SkillState.Ready:
                if (!weaponBase.isUsingSkill && weaponBase.weaponState != WeaponState.Attack)
                {
                    skillState = SkillState.Activate;
                }
                break;
            case SkillState.Activate:
                Activate();
                activateTime = activateTimeSetting;
                skillState = SkillState.UsingSkill;
                weaponBase.isUsingSkill = true;
                break;
            case SkillState.UsingSkill:
                if (activateTime > 0)
                    activateTime -= Time.deltaTime;
                else
                {
                    coldDown = coldDownSetting;
                    skillState = SkillState.ColdDown;
                }
                break;
            case SkillState.ColdDown:
                weaponBase.isUsingSkill = false;
                if (coldDown > 0)
                    coldDown -= Time.deltaTime;
                else
                {
                    skillState = SkillState.Ready;
                }
                break;
            default:
                break;
        }
    }
    public override void Activate()
    {
        base.Activate();
        StopAllCoroutines();
        StartCoroutine(WaitToSpawnBullet());
    }
    IEnumerator WaitToSpawnBullet() {
        staff.anim.SetTrigger("shot");
        yield return new WaitForSeconds(activateTime);
        StopAllCoroutines();
        StartCoroutine(Shot());
    }
    IEnumerator Shot() {
        for (int i = 0; i < bulletCount; i++)
        {
            yield return new WaitForSeconds(0.6f / bulletCount);
            GameObject bulletCreated = Instantiate(staff.bullet, staff.shotPoint.position, Quaternion.identity);
            Transform targetTransForm = GameObject.FindGameObjectWithTag("Player").transform;
            BulletHomming bulletHome = bulletCreated.GetComponent<BulletHomming>();
            bulletHome.Settarget(targetTransForm.position);
            bulletHome.weaponData = staff.weaponData;
        }
        staff.anim.ResetTrigger("shot");
    }
}
