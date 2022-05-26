using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MummyEnemy : BaseEnemy
{
    [Header("Mummy")]
    public MaxMinRange distanceToUseSkill;
    public bool wantToUseSkill;
    public override void MoveBack()
    {
        if (wantToUseSkill)
        {
            property.m_Speed = 2f;
            state = ActorState.Move;
            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            targetPoint = transform.position - (playerPosition - transform.position) * 2;
            Collider2D hitMax = Physics2D.OverlapCircle(transform.position, distanceToUseSkill.maxRange, whatIsPlayer);
            Collider2D hitMin = Physics2D.OverlapCircle(transform.position, distanceToUseSkill.minRange, whatIsPlayer);
            if (hitMin == null && hitMax != null)
            {
                moveBack = false;
            }
        }
        else
            base.MoveBack();

    }
    public override void CheckEnemyStop()
    {
        if (moveBack)
        {
            MoveBack();
            return;
        }
        property.m_Speed = 1f;
        if (wantToUseSkill)
        {
            Collider2D hitMax = Physics2D.OverlapCircle(transform.position, distanceToUseSkill.maxRange, whatIsPlayer);
            Collider2D hitMin = Physics2D.OverlapCircle(transform.position, distanceToUseSkill.minRange, whatIsPlayer);
            if (hitMin == null && hitMax != null)
            {
                canUseSkill = true;
                state = ActorState.Idle;
            }
            else if (hitMin != null && hitMax != null)
            {
                moveBack = true;
                canUseSkill = false;
                return;
            }
            else
            {
                state = ActorState.Move;
            }
            targetPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
            return;
        }
        canUseSkill = false;
        base.CheckEnemyStop();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, distanceToUseSkill.maxRange);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, distanceToUseSkill.minRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceStop.maxRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceStop.minRange);
    }
}
