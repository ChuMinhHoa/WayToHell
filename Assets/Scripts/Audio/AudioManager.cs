using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public List<SoundData> sounds;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
        InitData();
    }
    void InitData() {
        foreach (SoundData soundData in sounds)
        {
            soundData.source = gameObject.AddComponent<AudioSource>();
            soundData.source.clip = soundData.soundClip;
            soundData.source.volume = soundData.volume;
            soundData.source.pitch = soundData.pitch;
        }
    }
    private void Start()
    {
        //Check Map data ==> play theme
        Play(SoundName.Saharaz);
    }
    public void Play(SoundName soundName) {
        SoundData soundData = sounds.Find(
            delegate (SoundData s)
            {
                return s.soundName == soundName;
            }
        );
        soundData.source.Play();
    }
}
