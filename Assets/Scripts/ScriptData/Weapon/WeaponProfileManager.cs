using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProfileManager : MonoBehaviour
{
    public static WeaponProfileManager instance;
    private void Awake()
    {
        instance = this;
    }
    public WeaponData weaponData;
    public BulletProfile bulletProfile;
    
}
