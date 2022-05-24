using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBullet : BulletBase
{
    [Header("Sand bullet")]
    public float distance = 0f;
    public Animator anim;
    Vector3 startPosition;
    public override void Start()
    {
        base.Start();
        startPosition = transform.position;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        distance = Vector3.Distance(transform.position, startPosition);
        if (distance >= 1.5f)
        {
            bulletData.bulletSpeed = 0;
            rb_Bullet.velocity = Vector2.zero;
            anim.SetTrigger("EndActive");
            Destroy(gameObject, 0.18f);
        }
    }
    public override void ImpactOther()
    {
        base.ImpactOther();
        Collider2D hit = Physics2D.OverlapCircle(transform.position, impactRange, whatCanDamaged);
        if (hit != null)
        {
            ActorBase thisActor = hit.GetComponent<ActorBase>();
            float damage = weaponData.GetDamaged();
            thisActor.MinusHealth(damage);
            Vector3 direction = (hit.transform.position - transform.position).normalized * bulletData.knockBackFloat;
            thisActor.KnockBack(direction, bulletData.knockBackTime);
            anim.SetTrigger("EndActivate");
            Destroy(gameObject, 0.18f);
        }
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, impactRange);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].gameObject.layer > 6)
            {
                anim.SetTrigger("EndActivate");
                Destroy(gameObject, 0.18f);
            }
        }
    }

}
