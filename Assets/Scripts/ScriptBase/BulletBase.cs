using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public BulletType bulletType;
    public SAOWeaponData weaponData;
    public BulletData bulletData;
    public Rigidbody2D rb_Bullet;
    public int whatCanDamaged;
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
    }
    public virtual void BulletMove() {
        rb_Bullet.velocity = transform.right * bulletData.bulletSpeed * Time.deltaTime;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == whatCanDamaged)
        {
            ActorBase thisActor = collision.GetComponent<ActorBase>();
            float damage = weaponData.GetDamaged();
            thisActor.MinusHealth(damage);
            Vector3 direction = (collision.transform.position - transform.position).normalized * bulletData.knockBackFloat;
            thisActor.KnockBack(direction, bulletData.knockBackTime);
            Debug.LogWarning("Actor: " + collision.name + "; Damaged: " + damage);
        }
        if (collision.gameObject.layer != 6)
            Destroy(gameObject);
    }
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, impactRange);
    }
}
