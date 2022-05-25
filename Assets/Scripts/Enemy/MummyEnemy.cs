using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MummyEnemy : BaseEnemy
{
    [Header("Mummy")]
    public MaxMinRange distanceToUseSkill;
    public override void CheckEnemyStop()
    {
        base.CheckEnemyStop();
        Collider2D hitMax = Physics2D.OverlapCircle(transform.position,distanceToUseSkill.maxRange, whatIsPlayer);
        Collider2D hitMin = Physics2D.OverlapCircle(transform.position,distanceToUseSkill.minRange, whatIsPlayer);
        if (hitMin == null && hitMax != null)
            canUseSkill = true;
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
    }
}
