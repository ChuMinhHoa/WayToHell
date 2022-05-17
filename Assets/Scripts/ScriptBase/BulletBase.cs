using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public BulletType bulletType;
    public BulletData bulletData;
    public Rigidbody2D rb_Bullet;
    public LayerMask whatCanDamaged;
    public float impactRange;
    public virtual void Start() {
        InitData();
    }
    public virtual void InitData() {
        bulletData.InitData(WeaponProfileManager.instance.bulletProfile.GetBulletData(bulletType));
        Destroy(gameObject, 2f);
    }
    public virtual void FixedUpdate() {
        BulletMove();
        ImpactOther();
    }
    public virtual void BulletMove() {
        rb_Bullet.velocity = transform.right * bulletData.bulletSpeed * Time.deltaTime;
    }
    public virtual void ImpactOther() {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, impactRange, whatCanDamaged);
        if (hit != null)
        {
            ActorBase thisActor = hit.GetComponent<ActorBase>();
            float damage = bulletData.GetDamaged();
            thisActor.MinusHealth(damage);
            Vector3 direction = (hit.transform.position - transform.position).normalized * bulletData.knockBackFloat;
            thisActor.KnockBack(direction, bulletData.knockBackTime);
            Debug.LogWarning("Actor: " + hit.name + "; Damaged: " + damage);
            Destroy(gameObject);
        }
    }
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, impactRange);
    }
}
