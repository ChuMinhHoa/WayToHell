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
        GameObject effectObj = Instantiate(effectData.effect, position, Quaternion.identity);
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
}
