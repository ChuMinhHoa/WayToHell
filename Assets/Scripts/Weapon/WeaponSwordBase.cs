using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwordBase : WeaponBase
{
    public override void WeaponAttack(float angle)
    {
        base.WeaponAttack(angle);
        SwordAttack();
    }
    public virtual void SwordAttack() {
        Debug.Log("Sword Attack");
    }
}
