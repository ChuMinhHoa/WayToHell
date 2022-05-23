using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SoundName { 
    pistolShot,
    sword,
    themeMenu,
    Saharaz,
    hurt
}
[System.Serializable]
public class SoundData
{
    public SoundName soundName;
    public AudioClip soundClip;
    [Range(0, 1)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    [HideInInspector]
    public AudioSource source;
}
