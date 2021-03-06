using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHomming : BulletBase
{
    public AnimationCurve positionCurve, noiseCurve;
    public float timeSetting;
    public float speed;
    public MaxMinRange magnitudeRange;

    private Vector2 targetPoint, startPoint;
    private Vector2 horizontalVector;
    private float noisePosition;
    private float time;
    private float magnitude;
    public EffectName effectCreateAffterImpact;
    public float damage;
    public override void Start()
    {
        time = 0f;
        startPoint = transform.position;
        Vector2 direction = targetPoint - startPoint;
        horizontalVector = Vector2.Perpendicular(direction);
        magnitude = Random.Range(magnitudeRange.minRange, magnitudeRange.maxRange);
    }
    public override void FixedUpdate()
    {
        if (time < timeSetting)
        {
            noisePosition = noiseCurve.Evaluate(time);
            transform.position = Vector3.Lerp(startPoint, targetPoint, positionCurve.Evaluate(time)) +
                new Vector3(noisePosition * horizontalVector.x * magnitude, noisePosition * horizontalVector.y * magnitude);
            time += Time.deltaTime * speed;
            Vector2 nextPoint = Vector3.Lerp(startPoint, targetPoint, positionCurve.Evaluate(time)) +
                new Vector3(noiseCurve.Evaluate(time) * horizontalVector.x * magnitude, noiseCurve.Evaluate(time) * horizontalVector.y * magnitude);
            Vector2 direction = (nextPoint - (Vector2)transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
        else {
            EffectManager.instance.InstatiateEffect(EffectManager.instance.GetEffectData(effectCreateAffterImpact), transform.position);
            CameraControllerCustom.instance.ShakeScene(4f);
            Destroy(gameObject);
        }
        ImpactOther();
    }
    public override void ImpactOther()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, impactRange, whatCanDamaged);
        if (hit != null)
        {
            ActorBase thisActor = hit.GetComponent<ActorBase>();
            damage += weaponData.GetDamaged();
            thisActor.MinusHealth(damage);
            Vector3 direction = (hit.transform.position - transform.position).normalized * bulletData.knockBackFloat;
            thisActor.KnockBack(direction, bulletData.knockBackTime);
            EffectManager.instance.InstatiateEffect(EffectManager.instance.GetEffectData(effectCreateAffterImpact), transform.position);
            CameraControllerCustom.instance.ShakeScene(4f);
            Destroy(gameObject);
        }
    }
    public void Settarget(Vector2 target) {
        targetPoint = target;
        time = 0;
    }
}
