using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public List<TabGroupSpriteData> spriteDatas;
    TabButton currentSelect;
    private void Start()
    {
        InitData();
    }
    void InitData() {
        for (int i = 0; i < tabButtons.Count; i++)
        {
            tabButtons[i].InitData(this);
        }
    }
    public void OnTabSelect(TabButton tab) {
        tab.tabIcon.sprite = GetSpriteSelect(tab.tabName);
        tab.anim.SetBool("selected", true);
    }
    public void OnTabDeselect(TabButton tab) {
        tab.tabIcon.sprite = GetSpriteDeselect(tab.tabName);
        tab.anim.SetBool("selected", false);
    }
    public void Select(TabButton tab) {
        if (tab == currentSelect)
            return;
        currentSelect = tab;
        OnTabSelect(currentSelect);
        currentSelect.OnSelected();
        ResetTab();
    }
    public void DeSelect() {
        ResetTab();
    }
    public void ResetTab() {
        for (int i = 0; i < tabButtons.Count; i++)
        {
            if (tabButtons[i] == currentSelect)
                continue;
            tabButtons[i].OnDeSelected();
            OnTabDeselect(tabButtons[i]);
        }
    }
    public TabButton GetTabButton(ShopName shopName) {
        foreach (TabButton tab in tabButtons)
        {
            if (tab.tabName == shopName)
                return tab;
        }
        return null;
    }
    public Sprite GetSpriteSelect(ShopName tabName) {
        for (int i = 0; i < spriteDatas.Count; i++)
        {
            if (spriteDatas[i].shopName == tabName)
                return spriteDatas[i].selectedSprite;
        }
        return null;
    }
    public Sprite GetSpriteDeselect(ShopName tabName) {
        for (int i = 0; i < spriteDatas.Count; i++)
        {
            if (spriteDatas[i].shopName == tabName)
                return spriteDatas[i].normalSprite;
        }
        return null;
    }
}
