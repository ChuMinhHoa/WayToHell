using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SkillState { 
    Ready,
    Activate,
    UsingSkill,
    Hold,
    ColdDown
}
public class SkillBase : MonoBehaviour
{
    public string skillName;
    public float coldDownSetting;
    public float activateTimeSetting;
    public float coldDown;
    public float activateTime;
    public SkillState skillState;
    public KeyCode key;
    public float currentAngle;
    public WeaponBase weaponBase;
    public virtual void UpdateSkill() {
        if (weaponBase.isUsingSkill)
            return;
        switch (skillState)
        {
            case SkillState.Ready:
                if (Input.GetKeyDown(key))
                    skillState = SkillState.Activate;
                break;
            case SkillState.Activate:
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
    public virtual void Activate() { }
    public virtual void Activate(float angle) { }
}
