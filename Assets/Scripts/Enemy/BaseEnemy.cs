using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public enum EnemyType { 
    NormalEnemmy
}
public class BaseEnemy : ActorBase
{
    [Header("===========Enemy=========")]
    public EnemyType enemyType;
    public float distanceToStop;
    public Transform targetPlayerTransform;
    public float currentAimAngle;
    public Transform myAim;
    public SpriteRenderer avatarSpriteRenderer;
    [Header("==========PathFinder========")]
    Seeker seeker;
    Path path;
    public int currentWayPoint;
    public float distanceToNextWayPoint;
    public override void Start()
    {
        base.Start();
        property.LoadData(ProfileManager.instance.enemyProfile.GetEnemyData(enemyType).property);
        targetPlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("UpdatePath", 0, 0.5f);
        seeker = GetComponent<Seeker>();
    }
    public virtual void UpdatePath() {
        if (seeker.IsDone() && targetPlayerTransform != null)
            seeker.StartPath(rb.position, targetPlayerTransform.position, OnPathComplete);
    }
    public virtual void OnPathComplete(Path p) {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case ActorState.Idle:
                if (stateMachine.GetCurrentState() != IdleState.Instance)
                    stateMachine.SetCurrentState(IdleState.Instance);
                break;
            case ActorState.Move:
                if (stateMachine.GetCurrentState() != MoveState.Instance)
                    stateMachine.SetCurrentState(MoveState.Instance);
                break;
            case ActorState.Attack:
                Attack();
                if (stateMachine.GetCurrentState() != IdleState.Instance)
                    stateMachine.SetCurrentState(IdleState.Instance);
                break;
            case ActorState.Death:
                break;
            case ActorState.KnockBack:
                if (stateMachine.GetCurrentState() != KnockBackState.Instance)
                    stateMachine.SetCurrentState(KnockBackState.Instance);
                break;
            default:
                break;
        }
        AimFollowTarget();
        CheckEnemyStop();
    }
    #region Idle
    public override void OnIdleEnter()
    {
        base.OnIdleEnter();
    }
    public override void OnIdleExecute()
    {
        base.OnIdleExecute();
    }
    public override void OnIdleExit()
    {
        base.OnIdleExit();
    }
    #endregion
    #region Move
    float distanceToTarget = 0f;
    public override void OnMoveEnter()
    {
        base.OnMoveEnter();
    }
    public override void OnMoveExecute()
    {
        base.OnMoveExecute();
        CheckNextWayPoint();
    }
    public override void OnMoveExit()
    {
        base.OnMoveExit();
    }
    public virtual void CheckEnemyStop() {
        distanceToTarget = Vector3.Distance(transform.position, targetPlayerTransform.position);
        if (stateMachine.GetCurrentState() == KnockBackState.Instance)
            return;
        if (distanceToStop >= distanceToTarget)
            state = ActorState.Attack;
        else
            state = ActorState.Move;
    }
    public virtual void CheckNextWayPoint() {
        if (path == null)
            return;
        if (currentWayPoint >= path.vectorPath.Count)
            return;
        Vector3 nextWayPoint = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        if (targetPlayerTransform != null)
            Move(nextWayPoint);
    }
    public virtual void Move(Vector3 nextWayPoint) {
        float disNextWayPoint = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);
        movement = nextWayPoint;
        if (disNextWayPoint < distanceToNextWayPoint)
            currentWayPoint++;
        rb.MovePosition(rb.position + movement * property.m_Speed * Time.deltaTime);
    }
    #endregion
    public override void Attack()
    {
        base.Attack();
        if (currentWeapon.weaponState != WeaponState.ColdDown)
        {
            currentWeapon.aimAngle = currentAimAngle;
            currentWeapon.weaponState = WeaponState.Attack;
        }
    }
    public virtual void AimFollowTarget() {
        if (targetPlayerTransform != null)
        {
            Vector3 direction = targetPlayerTransform.position - transform.position;
            currentAimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            myAim.eulerAngles = new Vector3(0, 0, currentAimAngle);
            FlipCheck();
        }
    }
    void FlipCheck()
    {
        if ((avatarSpriteRenderer.flipX && targetPlayerTransform.position.x > transform.position.x) ||
            (!avatarSpriteRenderer.flipX && targetPlayerTransform.position.x < transform.position.x))
        {
            avatarSpriteRenderer.flipX = !avatarSpriteRenderer.flipX;
            Transform myAimTransform = myAim.transform;
            Vector3 scale = myAimTransform.localScale;
            scale.y *= -1;
            myAimTransform.localScale = scale;
        }
    }
    public override void KnockBack(Vector3 direction, float knockBackTime)
    {
        base.KnockBack(direction, knockBackTime);
        state = ActorState.KnockBack;
        rb.velocity = direction;
        StopAllCoroutines();
        StartCoroutine(ResetKnockBack(knockBackTime));
    }
    public override void Death()
    {
        base.Death();
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToStop);
    }
}

