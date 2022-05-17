using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ActorState { 
    Idle,
    Move,
    Attack,
    KnockBack,
    Death,
    Stun
}
public class ActorBase : MonoBehaviour
{
    public Property property;
    public StateMachine<ActorBase> stateMachine { get { return m_StateMachine; } }
    protected StateMachine<ActorBase> m_StateMachine;
    public ActorState state;
    public Rigidbody2D rb;
    public Animator anim;
    public Vector2 movement;
    public WeaponBase currentWeapon;
    public virtual void Start() {
        InitStateMachine();
        InitProperty();
    }
    public virtual void Update() {
        m_StateMachine.Update();
        DeathCheck();
    }
    public virtual void InitStateMachine() {
        m_StateMachine = new StateMachine<ActorBase>(this);
        m_StateMachine.SetCurrentState(IdleState.Instance);
    }
    public virtual void InitProperty() { }
    public virtual void OnIdleEnter() { }
    public virtual void OnIdleExecute() { }
    public virtual void OnIdleExit() { }
    public virtual void OnKnockBackEnter() { }
    public virtual void OnKnockBackExecute() { }
    public virtual void OnKnockBackEnd() { }
    public virtual void OnMoveEnter() { }
    public virtual void OnMoveExecute() { }
    public virtual void OnMoveExit() { }
    public virtual void Attack() { }
    public virtual void KnockBack(Vector3 direction, float timeKnockBack) { }
    public virtual IEnumerator ResetKnockBack(float knockBackTime) {
        yield return new WaitForSeconds(knockBackTime);
        state = ActorState.Idle;
        rb.velocity = Vector2.zero;
    }
    public virtual void AddHealth(float value) {
        property.AddHealth(value);
    }
    public virtual void MinusHealth(float value) {
        property.MinusHealth(value);
    }
    public virtual void AddShield(float value)
    {
        property.AddHealth(value);
    }
    public virtual void MinusShield(float value)
    {
        property.MinusHealth(value);
    }
    public virtual void AddMaxHealth(float value)
    {
        property.AddHealth(value);
    }
    public virtual void MinusMaxHealth(float value)
    {
        property.MinusHealth(value);
    }
    public virtual void AddMaxShield(float value)
    {
        property.AddHealth(value);
    }
    public virtual void MinusMaxShield(float value)
    {
        property.MinusHealth(value);
    }
    public virtual void DeathCheck() {
        if (property.m_Health == 0)
            Death();
    }
    public virtual void Death() {
        Debug.Log(gameObject.name + " die.");
    }
}
public class IdleState : State<ActorBase> {
    private static IdleState m_Instance = null;
    public static IdleState Instance {
        get {
            if (m_Instance == null)
                m_Instance = new IdleState();
            return m_Instance;
        }
    }

    public override void Enter(ActorBase go)
    {
        go.OnIdleEnter();
    }

    public override void Execute(ActorBase go)
    {
        go.OnIdleExecute();
    }

    public override void Exit(ActorBase go)
    {
        go.OnIdleExit();
    }
}
public class MoveState : State<ActorBase> {
    private static MoveState m_Instance = null;
    public static MoveState Instance{
        get {
            if (m_Instance == null)
                m_Instance = new MoveState();
            return m_Instance;
        }
    }
    public override void Enter(ActorBase go)
    {
        go.OnMoveEnter();
    }
    public override void Execute(ActorBase go)
    {
        go.OnMoveExecute();
    }
    public override void Exit(ActorBase go)
    {
        go.OnMoveExit();
    }
}
public class KnockBackState : State<ActorBase>
{
    private static KnockBackState m_Instance = null;
    public static KnockBackState Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = new KnockBackState();
            return m_Instance;
        }
    }
    public override void Enter(ActorBase go)
    {
        go.OnKnockBackEnter();
    }
    public override void Execute(ActorBase go)
    {
        go.OnKnockBackExecute();
    }
    public override void Exit(ActorBase go)
    {
        go.OnKnockBackEnd();
    }
}
