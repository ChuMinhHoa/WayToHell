using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSkill : SkillBase
{
    [Header("Sand skill")]
    public Animator anim;
    public MummyEnemy mummy;
    public override void UpdateSkill()
    {
        switch (skillState)
        {
            case SkillState.Ready:
                if (!weaponBase.isUsingSkill) {
                    if (mummy.MoveBackCheck())
                        mummy.targetPlayerPoint = mummy.InitPointToMoveBack();
                    else { 
                        skillState = SkillState.Activate;
                    }
                } 
                break;
            case SkillState.Activate:
                Activate();
                activateTime = activateTimeSetting;
                skillState = SkillState.UsingSkill;
                
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
    public override void Activate()
    {
        base.Activate();
        anim.SetTrigger("activate");
    }
}
