using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSceneUI : PanelWrap
{
    public ProgressBar loadingBar;
    public override void OnInit()
    {
        base.OnInit();
        loadingBar.maxValue = 1;
        loadingBar.Changeprogress(0);
    }
    public void ChangeLoadingBar(float value) {
        loadingBar.Changeprogress(value);
    }
}
