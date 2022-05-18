using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwordBase : WeaponBase
{
    [Header("================Sword==============")]
    public Transform checkDamagePoint;
    public float checkDamageRange;
    public LayerMask whatCanDamage;
    public float knockBackFloat;
    public float knockBackTime;
    public override void WeaponAttack(float angle)
    {
        base.WeaponAttack(angle);
        anim.SetTrigger("attack");
        StopAllCoroutines();
        StartCoroutine(ImpactOtherCheck());
    }

    public virtual IEnumerator ImpactOtherCheck() {
        yield return new WaitForSeconds(animAttackTime);
        Collider2D hit = Physics2D.OverlapCircle(checkDamagePoint.position, checkDamageRange, whatCanDamage);
        if (hit != null)
        {
            ActorBase thisActor = hit.GetComponent<ActorBase>();
            float damage = weaponData.GetDamaged();
            thisActor.MinusHealth(damage);
            EffectManager.instance.InstatiateEffect(EffectManager.instance.GetEffectData(EffectName.SwordHitEffect), hit.transform.position);
            Vector3 direction = (hit.transform.position - checkDamagePoint.position).normalized * knockBackFloat;
            thisActor.KnockBack(direction, knockBackTime);
            Debug.LogWarning("Actor: " + hit.name + "; Damaged: " + damage);
        }
        anim.ResetTrigger("attack");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkDamagePoint.position, checkDamageRange);
    }
}
