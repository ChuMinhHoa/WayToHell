using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvas : PanelWrap
{
    public List<PanelWrap> menuWraps;
    public void InitData()
    {
        for (int i = 0; i < menuWraps.Count; i++)
            menuWraps[i].OnClose();
        menuWraps[0].OnOpen();
    }
}
