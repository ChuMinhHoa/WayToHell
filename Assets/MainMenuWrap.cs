using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuWrap : PanelWrap
{
    public Button btnPlay;
    public Button btnOption;
    public Button btnCredit;
    public Button btnExit;
    public override void OnInit()
    {
        base.OnInit();
        btnPlay.onClick.AddListener(BtnPlay);
        btnOption.onClick.AddListener(BtnOption);
        btnCredit.onClick.AddListener(BtnCredit);
        btnExit.onClick.AddListener(BtnExit);
    }
    void BtnPlay() {
        ChangeSceneManager.instance.ChangeScene(1);
    }
    void BtnOption() { }
    void BtnCredit() { }
    void BtnExit() { }
}
