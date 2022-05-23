using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Property {
    public float m_MaxHealth;
    public float m_MaxShield;
    public float m_Shield;
    public float m_Health;
    public float m_Speed;
    public void LoadData(Property property) {
        m_MaxHealth = property.m_MaxHealth;
        m_MaxShield = property.m_MaxShield;
        m_Shield = property.m_Shield;
        m_Health = property.m_Health;
        m_Speed = property.m_Speed;
    }
    public void MinusHealth(float value) {
        m_Health -= value;
        if (m_Health < 0)
            m_Health = 0;
    }
    public void MinusShield(float value) {
        m_Shield -= value;
        if (m_Shield < 0)
            m_Shield = 0;
    }
    public void MinusMaxHealth(float value) {
        m_MaxHealth -= value;
        if (m_MaxHealth < 0)
            m_MaxHealth = 0;
    }
    public void MinusMaxShield(float value)
    {
        m_MaxShield -= value;
        if (m_MaxShield < 0)
            m_MaxShield = 0;
    }
    public void AddHealth(float value) {
        m_Health += value;
        if (m_Health > m_MaxHealth)
            m_Health = m_MaxHealth;
    }
    public void AddShield(float value) {
        m_Shield += value;
        if (m_Shield > m_MaxShield)
            m_Shield = m_MaxHealth;
    }
    public void AddMaxHealth(float value)
    {
        m_MaxHealth += value;
    }
    public void AddMaxShield(float value)
    {
        m_MaxShield += value;
    }
}
[System.Serializable]
public class PlayerProfile
{
    public float gem;
    public float coin;
    public Property property;
    public List<WeaponType> weapons;
    public int mapIndex;
    public int levelInMap;
    public void SaveProfile() 
    {
        property = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>().property;
        PlayerPrefs.SetString(Keys.playerProfile,JsonUtility.ToJson(this));
    }
    public void LoadProfile() {
        SaveProfile();
        if (PlayerPrefs.HasKey(Keys.playerProfile))
        {
            string jsonProfile = PlayerPrefs.GetString(Keys.playerProfile);
            PlayerProfile playerProfile = JsonUtility.FromJson<PlayerProfile>(jsonProfile);
            coin = playerProfile.coin;
            gem = playerProfile.gem;
            property.LoadData(playerProfile.property);
            weapons = playerProfile.weapons;
            mapIndex = playerProfile.mapIndex;
            levelInMap = playerProfile.levelInMap;
        }
        else { SaveProfile(); }
    }
    public void AddWeapon(WeaponType weaponType) {
        weapons.Add(weaponType);
    }
    public void ChangeWeapon(WeaponType weaponType, int weaponChangeIndex) {
        weapons[weaponChangeIndex] = weaponType;
    }
}
