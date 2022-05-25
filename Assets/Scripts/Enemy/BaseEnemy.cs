using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public enum EnemyType { 
    GunEnemy,
    SwordEnemy,
    MummyEnemy
}
public class BaseEnemy : ActorBase
{
    [Header("===========Enemy=========")]
    public EnemyType enemyType;
    public MaxMinRange distanceStop;
    public Vector3 targetPoint;
    public float currentAimAngle;
    public Transform myAim;
    [Header("==========PathFinder========")]
    Seeker seeker;
    Path path;
    public int currentWayPoint;
    public float distanceToNextWayPoint;
    [HideInInspector]
    public bool canUseSkill;
    public override void Start()
    {
        base.Start();
        property.LoadData(ProfileManager.instance.enemyProfile.GetEnemyData(enemyType).property);
        targetPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
        InvokeRepeating("UpdatePath", 0, 0.1f);
        seeker = GetComponent<Seeker>();
    }
    public virtual void UpdatePath() {
        if (seeker.IsDone() && targetPoint != Vector3.zero)
            seeker.StartPath(rb.position, targetPoint, OnPathComplete);
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
    [HideInInspector]
    public bool moveBack;
    public LayerMask whatIsPlayer;
    public virtual void MoveBack()
    {
        state = ActorState.Move;
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        targetPoint = transform.position - (playerPosition - transform.position) * 2;
        Collider2D hitMax = Physics2D.OverlapCircle(transform.position, distanceStop.maxRange, whatIsPlayer);
        Collider2D hitMin = Physics2D.OverlapCircle(transform.position, distanceStop.minRange, whatIsPlayer);
        if (hitMin == null && hitMax != null) {
            moveBack = false;
        }
    }
    public virtual void CheckEnemyStop() {
        Collider2D hitMax = Physics2D.OverlapCircle(transform.position, distanceStop.maxRange, whatIsPlayer);
        Collider2D hitMin = Physics2D.OverlapCircle(transform.position, distanceStop.minRange, whatIsPlayer);
        if (moveBack)
        {
            MoveBack();
            return;
        }
        if (hitMin == null && hitMax != null)
            state = ActorState.Attack;
        else if (hitMin != null && hitMax != null)
        {
            moveBack = true;
            return;
        }
        else
        {
            state = ActorState.Move;
        }
        targetPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
    public virtual void CheckNextWayPoint() {
        if (path == null)
            return;
        if (currentWayPoint >= path.vectorPath.Count)
            return;
        Vector3 nextWayPoint = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        if (targetPoint != Vector3.zero)
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
        if (targetPoint != Vector3.zero)
        {
            Vector3 direction = targetPoint - transform.position;
            currentAimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            myAim.eulerAngles = new Vector3(0, 0, currentAimAngle);
            currentWeapon.aimAngle = currentAimAngle;
            FlipCheck();
        }
    }
    void FlipCheck()
    {
        if ((avatar.flipX && targetPoint.x > transform.position.x) ||
            (!avatar.flipX && targetPoint.x < transform.position.x))
        {
            avatar.flipX = !avatar.flipX;
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
        GameManager.instance.spawnManager.MinusEnemiesWayCount();
        EffectManager.instance.InstatiateEffect(EffectManager.instance.GetEffectData(EffectName.DeathEffect),transform.position);
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceStop.maxRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceStop.minRange);
    }
}

