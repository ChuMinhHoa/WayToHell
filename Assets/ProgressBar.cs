using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : PanelWrap
{
    public Image progressBar;
    public float maxValue;
    public float currentValue;
    //value persent
    public void Changeprogress(float value) {
        currentValue = value;
        progressBar.fillAmount = currentValue / maxValue;
    }
}
