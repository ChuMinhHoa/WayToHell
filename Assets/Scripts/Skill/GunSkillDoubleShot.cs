using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSkillDoubleShot : SkillBase
{
    public GameObject bullet;
    public Transform shotPoint_1;
    public Transform shotPoint_2;
    public override void UpdateSkill()
    {
        base.UpdateSkill();
        switch (skillState)
        {
            case SkillState.Activate:
                weaponBase.anim.SetTrigger("shot");
                activateTime = activateTimeSetting;
                skillState = SkillState.UsingSkill;
                weaponBase.isUsingSkill = true;
                Activate();
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
                weaponBase.anim.ResetTrigger("shot");
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
        GameObject bulletCreated_1 = Instantiate(bullet, shotPoint_1.position, Quaternion.identity);
        bulletCreated_1.transform.eulerAngles = new Vector3(0, 0, currentAngle);
        bulletCreated_1.GetComponent<BulletBase>().weaponData = weaponBase.weaponData;
        GameObject bulletCreated_2 = Instantiate(bullet, shotPoint_2.position, Quaternion.identity);
        bulletCreated_2.transform.eulerAngles = new Vector3(0, 0, currentAngle);
        bulletCreated_2.GetComponent<BulletBase>().weaponData = weaponBase.weaponData;
    }
}
