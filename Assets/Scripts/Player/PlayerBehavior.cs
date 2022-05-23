using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerBehavior : ActorBase
{
    [Header("==========Player==========")]
    public PlayerAimFollowMousePosition aimFollowMousePosition;
    public List<SAOWeaponData> weapons;
    public GameObject handEquip;
    public override void Start()
    {
        base.Start();
        InitWeapon();
    }
    public override void Update()
    {
        base.Update();
        if (state == ActorState.Death)
            return;
        if (state == ActorState.Stun)
            return;
        InputHandle();
        currentWeapon.aimAngle = aimFollowMousePosition.angle;
    }
    public override void InitProperty()
    {
        base.InitProperty();
        property = ProfileManager.instance.playerProfile.property;
    }
    public void InitWeapon() {
        ClearWeapon();
        weapons.Clear();
        List<WeaponType> weaponTypes = ProfileManager.instance.playerProfile.weapons;
        for (int i = 0; i < weaponTypes.Count; i++)
            weapons.Add(WeaponProfileManager.instance.weaponData.GetWeaponData(weaponTypes[i]));
        CreateWeapon(0);
    }
    public virtual void ClearWeapon() {
        Transform[] weaponObjectsCurrent = handEquip.GetComponentsInChildren<Transform>();
        if (weaponObjectsCurrent.Length > 1)
            for (int i = 1; i < weaponObjectsCurrent.Length; i++)
                Destroy(weaponObjectsCurrent[i].gameObject);
    }
    public virtual void CreateWeapon(int indexWeaponCreate) {
        GameObject weaponCreate = Instantiate(weapons[indexWeaponCreate].weaponObject, handEquip.transform.position, Quaternion.identity, handEquip.transform);
        WeaponBase weaponBase = weaponCreate.GetComponent<WeaponBase>();
        currentWeapon = weaponBase;
    }
    void InputHandle() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movement = new Vector2(horizontal, vertical);
        if (movement != Vector2.zero)
            state = ActorState.Move;
        else state = ActorState.Idle;
        if (state != ActorState.Death && Input.GetMouseButtonDown(0))
            Attack();
        if (Input.GetKeyDown(KeyCode.Q))
            SwitchWeapon();
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
        rb.MovePosition(rb.position + movement * ProfileManager.instance.playerProfile.property.m_Speed * Time.fixedDeltaTime);
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
        ProfileManager.instance.playerProfile.SaveProfile();
    }
    public override void MinusHealth(float value)
    {
        base.MinusHealth(value);
        ProfileManager.instance.playerProfile.SaveProfile();
        CameraControllerCustom.instance.PlayerHurt();
        AudioManager.instance.Play(SoundName.hurt);
    }
    public override void Death()
    {
        base.Death();
        aimFollowMousePosition.PlayerDie();
    }
    #region==========Weapon===============
    public void SwitchWeapon() {
        if (CurrentWeaponIndex() == -1)
            return;
        if (CurrentWeaponIndex() == 1)
        {
            ClearWeapon();
            CreateWeapon(0);
        }
        else
        {
            ClearWeapon();
            CreateWeapon(1);
        }
    }
    public int CurrentWeaponIndex() {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (currentWeapon.weaponType == weapons[i].weaponType)
                return i;
        }
        return -1;
    }
    public void ChangeWeapon(SAOWeaponData weaponData, int weaponRemoveIndex) {
        if (weaponRemoveIndex < weapons.Count)
            ProfileManager.instance.playerProfile.ChangeWeapon(weaponData.weaponType, weaponRemoveIndex);
        else return;
        InitWeapon();
    }
    public void AddWeapon(SAOWeaponData weaponData) {
        ProfileManager.instance.playerProfile.AddWeapon(weaponData.weaponType);
        InitWeapon();
    }
    public bool CheckWeaponSlotFull(){
        return weapons.Count == 2;
    }
    public void RemoveWeapon(WeaponType weaponType) {
        foreach (SAOWeaponData data in weapons)
        {
            if (data.weaponType == weaponType)
            {
                weapons.Remove(data);
                break;
            }
        }
    }
    #endregion
}
