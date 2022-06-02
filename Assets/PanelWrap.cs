using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelWrap : MonoBehaviour
{
    public virtual void OnInit() { }
    public virtual void OnOpen() {
        this.gameObject.SetActive(true);
        OnInit();
    }
    public virtual void OnClose() {
        this.gameObject.SetActive(false);
    }
}
