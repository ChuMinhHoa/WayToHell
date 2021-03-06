using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public WeaponState weaponState;
    public WeaponType weaponType;
    public SAOWeaponData weaponData;
    public float aimAngle;
    private float coldDown;
    public Animator anim;
    [Range(0, 2)]
    public float animAttackTime;
    public List<SkillBase> skillBases;
    public bool isUsingSkill;
    public virtual void Start() {
        InitData();
    }
    public virtual void Update() {
        foreach (SkillBase skill in skillBases)
        {
            skill.currentAngle = aimAngle;
            skill.UpdateSkill();
        }
    }
    public virtual void FixedUpdate()
    {
        if (!isUsingSkill)
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
    }
    public virtual void InitData() {
        weaponData.InitData(WeaponProfileManager.instance.weaponData.GetWeaponData(weaponType));
        transform.rotation = new Quaternion(0, 0, 0, 0);
        foreach (SkillBase skill in skillBases)
        {
            skill.weaponBase = this;
        }
    }
    public virtual void WeaponIdle() { }
    public virtual void WeaponAttack(float aimAngle) { }
}
