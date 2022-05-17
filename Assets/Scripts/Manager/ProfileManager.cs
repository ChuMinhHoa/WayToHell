using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    public static ProfileManager instance;
    public PlayerProfile playerProfile = new PlayerProfile();
    public EnemyProfileData enemyProfile; 
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
        playerProfile.LoadProfile();
    }
}
