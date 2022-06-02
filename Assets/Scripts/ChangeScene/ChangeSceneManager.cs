using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneManager : MonoBehaviour
{
    public static ChangeSceneManager instance;
    private void Awake()
    {
        instance = this;
    }
    AsyncOperation sceneLoading =new AsyncOperation();
    float totalSceneProgress;
    public void ChangeScene(int sceneIndex) {
        UIManager.instance.OnOpenChangeScene();
        sceneLoading = SceneManager.LoadSceneAsync(sceneIndex);
        StopAllCoroutines();
        StartCoroutine(LoadSceneProgress());
    }
    IEnumerator LoadSceneProgress() {
        while (!sceneLoading.isDone)
        {
            totalSceneProgress = 0;
            totalSceneProgress = sceneLoading.progress;
            UIManager.instance.changeSceneUI.ChangeLoadingBar(totalSceneProgress);
            yield return null;
        }
        UIManager.instance.OnCloseChangeScene();
    }
}
