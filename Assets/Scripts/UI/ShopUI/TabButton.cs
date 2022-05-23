using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public enum ShopName {
    Weapon,
    Item,
    Gem
}
public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    public ShopName tabName;
    TabGroup tabGroup;
    public Image backGround;
    public Image tabIcon;
    public Animator anim;
    public UnityEvent onSelect, onDeselect;
    public void InitData(TabGroup _tabGroup) {
        tabGroup = _tabGroup;
        if (transform.GetSiblingIndex()==0)
        {
            tabGroup.Select(this);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.Select(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.DeSelect();
    }
    public void OnSelected() {
        if (onSelect != null)
            onSelect.Invoke();
    }
    public void OnDeSelected() {
        if (onDeselect != null)
            onDeselect.Invoke();
    }
}
