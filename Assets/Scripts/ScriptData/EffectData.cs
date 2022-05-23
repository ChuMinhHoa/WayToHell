using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EffectName { 
    SwordHitEffect,
    DeathEffect,
    EnemySpawnEffect,
    MummySpawnEffect
}
[System.Serializable]
public class EffectData
{
    public GameObject effect;
    public EffectName effectName;
    public float effectTimeAnim;
}
