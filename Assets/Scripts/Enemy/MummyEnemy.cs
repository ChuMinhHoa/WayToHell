using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyEnemy : BaseEnemy
{
    [Header("Mummy")]
    public bool moveBack;
    public float distanceToUsingSkill;
    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        if (moveBack)
        {
            state = ActorState.Move;
            return;
        }
        else { 
            targetPlayerPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
        base.Update();
    }
    public Vector3 InitPointToMoveBack() {
        return transform.position - (targetPlayerPoint - transform.position);
    }
    public bool MoveBackCheck() {
        distanceToTarget = Vector3.Distance(transform.position, targetPlayerPoint);
        if (distanceToTarget < distanceToUsingSkill)
        {
            moveBack = true;
            return true;
        }
        moveBack = false;
        return false;
    }
}
