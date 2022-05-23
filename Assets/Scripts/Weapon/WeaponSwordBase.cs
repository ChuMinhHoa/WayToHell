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
        Collider2D[] hits = Physics2D.OverlapCircleAll(checkDamagePoint.position, checkDamageRange, whatCanDamage);
        for (int i = 0; i < hits.Length; i++)
        {
            ActorBase thisActor = hits[i].GetComponent<ActorBase>();
            float damage = weaponData.GetDamaged();
            thisActor.MinusHealth(damage);
            EffectManager.instance.InstatiateEffect(EffectManager.instance.GetEffectData(EffectName.SwordHitEffect), hits[i].transform.position);
            Vector3 direction = (hits[i].transform.position - checkDamagePoint.position).normalized * knockBackFloat;
            thisActor.KnockBack(direction, knockBackTime);
            AudioManager.instance.Play(SoundName.sword);
        }
        anim.ResetTrigger("attack");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkDamagePoint.position, checkDamageRange);
    }
}
