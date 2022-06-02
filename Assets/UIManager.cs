using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public PanelWrap menuWrap;
    public PanelWrap isActivate;
    public PanelWrap gamePlayPanel;
    private void Awake()
    {
        instance = this;
    }
    public MenuCanvas menuCanvas;
    public ChangeSceneUI changeSceneUI;
    private void Start()
    {
        InitData();
        OnOpenMenuUI();
    }
    void InitData() {
        menuCanvas.InitData();
    }
    public void OnOpenMenuUI() {
        if (isActivate != null)
            isActivate.OnClose();
        menuWrap.OnOpen();
        isActivate = menuWrap;
    }
    public void OnCloseMenuUI() {
        menuWrap.OnClose();
        isActivate = null;
    }
    public void OnOpenChangeScene() {
        if (isActivate != null)
            isActivate.OnClose();
        changeSceneUI.OnOpen();
        isActivate = changeSceneUI;
    }
    public void OnCloseChangeScene()
    {
        changeSceneUI.OnClose();
        isActivate = null;
    }
}
