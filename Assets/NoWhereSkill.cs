using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoWhereSkill : SkillBase 
{
    public Animator anim;
    public GameObject noWhereBullet;
    public Transform shotPoint;
    public float damage;
    public override void UpdateSkill()
    {
        base.UpdateSkill();
        switch (skillState)
        {
            case SkillState.Ready:
                break;
            case SkillState.Activate:
                Activate();
                break;
            case SkillState.UsingSkill:
                if (activateTime <= 0f)
                    CreateNoWhereBullet();
                break;
            case SkillState.Hold:
                break;
            case SkillState.ColdDown:
                break;
            default:
                break;
        }
    }
    public override void Activate()
    {
        base.Activate();
        anim.SetTrigger("nowhere");
    }
    public void CreateNoWhereBullet() {
        GameObject nowhereBullet = Instantiate(noWhereBullet, shotPoint.position, Quaternion.identity);
        BulletHomming bullet = nowhereBullet.GetComponent<BulletHomming>();
        bullet.weaponData = weaponBase.weaponData;
        bullet.Settarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        bullet.damage = damage;
    }
}
