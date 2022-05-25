using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSkill : SkillBase
{
    [Header("Sand skill")]
    public Animator anim;
    public MummyEnemy mummy;
    public GameObject sandSkillBullet;
    public Transform sandBulletSpawnPoint;
    public override void UpdateSkill()
    {
        switch (skillState)
        {
            case SkillState.Ready:
                if (!weaponBase.isUsingSkill ) {
                    skillState = SkillState.Activate;
                } 
                break;
            case SkillState.Activate:
                Activate(currentAngle);
                activateTime = activateTimeSetting;
                skillState = SkillState.UsingSkill;
                weaponBase.isUsingSkill = true;
                break;
            case SkillState.UsingSkill:
                if (activateTime > 0)
                    activateTime -= Time.deltaTime;
                else
                {
                    GameObject sandBulletObject = Instantiate(sandSkillBullet, sandBulletSpawnPoint.position, Quaternion.identity);
                    BulletBase sandBullet = sandBulletObject.GetComponent<BulletBase>();
                    sandBulletObject.transform.eulerAngles = new Vector3(0, 0, currentAngle);
                    sandBullet.weaponData = weaponBase.weaponData;
                    coldDown = coldDownSetting;
                    skillState = SkillState.ColdDown;
                }
                break;
            case SkillState.ColdDown:
                anim.ResetTrigger("activate");
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
    public override void Activate(float angle)
    {
        base.Activate(angle);
        anim.SetTrigger("activate");
    }
}
