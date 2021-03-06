using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;
    public List<EffectData> effectDatas;
    public void Awake()
    {
        instance = this;
    }
    public void InstatiateEffect(EffectData effectData, Vector3 position) {
        if (effectData == null)
            return;
        GameObject effectObj = Instantiate(effectData.effect, position, effectData.effect.transform.rotation);
        Destroy(effectObj, effectData.effectTimeAnim);
    }
    public EffectData GetEffectData(EffectName effectName) {
        foreach (EffectData data in effectDatas)
        {
            if (data.effectName == effectName)
                return data;
        }
        return null;
    }
    public EffectData GetEffectData(EnemyType enemyType) {
        switch (enemyType)
        {
            case EnemyType.GunEnemy:
                return GetEffectData(EffectName.EnemySpawnEffect);
            case EnemyType.SwordEnemy:
                return GetEffectData(EffectName.EnemySpawnEffect);
            case EnemyType.MummyEnemy:
                return GetEffectData(EffectName.MummySpawnEffect);
            default:
                return null;
        }
    }
}
