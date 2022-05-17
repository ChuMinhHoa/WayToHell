using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerBehavior : ActorBase
{
    public PlayerAimFollowMousePosition aimFollowMousePosition;
    public override void Update()
    {
        base.Update();
        if (state == ActorState.Death)
            return;
        if (state == ActorState.Stun)
            return;
        InputHandle();
    }
    public override void InitProperty()
    {
        base.InitProperty();
        property = ProfileManager.instance.playerProfile.property;
    }
    void InputHandle() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movement = new Vector2(horizontal, vertical);
        if (movement != Vector2.zero)
            state = ActorState.Move;
        if (state != ActorState.Death && Input.GetMouseButtonDown(0))
            Attack();
    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case ActorState.Idle:
                stateMachine.SetCurrentState(IdleState.Instance);
                break;
            case ActorState.Move:
                stateMachine.SetCurrentState(MoveState.Instance);
                break;
            case ActorState.Death:
                return;
            case ActorState.Stun:
                break;
            default:
                break;
        }
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
        rb.MovePosition(rb.position + movement * ProfileManager.instance.playerProfile.property.m_Speed * Time.deltaTime);
    }
    public override void OnMoveExit()
    {
        base.OnMoveExit();
    }
    #endregion
    public override void KnockBack(Vector3 direction, float knockBackTime)
    {
        base.KnockBack(direction, knockBackTime);
        state = ActorState.Stun;
        StopAllCoroutines();
        StartCoroutine(ResetKnockBack(knockBackTime));
    }
    public override void Attack()
    {
        base.Attack();
        if (currentWeapon.weaponState != WeaponState.ColdDown)
        {
            currentWeapon.aimAngle = aimFollowMousePosition.angle;
            currentWeapon.weaponState = WeaponState.Attack;
        }
    }
    public override void AddHealth(float value)
    {
        base.AddHealth(value);
        ProfileManager.instance.playerProfile.SaveProfile(this.property);
    }
}
