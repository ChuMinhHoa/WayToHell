using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public enum CameraState { 
    idle,
    increaseViewFar,
    resetView
}
public class CameraControllerCustom : MonoBehaviour
{
    public static CameraControllerCustom instance;
    public CinemachineVirtualCamera cinemachineCam;
    public CinemachineConfiner cameraConfiner;
    public GameObject hurtPanel;
    float viewfar;
    public float viewfarDefault;
    public float viewfarPoint;
    public float viewfarChangeSpeed;
    public CameraState cameraState;
    private void Awake()
    {
        instance = this;
        InitData();
    }
    void InitData() {
        viewfarDefault = cinemachineCam.m_Lens.OrthographicSize;
        viewfar = viewfarDefault;
    }
    private void FixedUpdate()
    {
        switch (cameraState)
        {
            case CameraState.increaseViewFar:
                IncreaseViewFar(viewfarPoint);
                break;
            case CameraState.resetView:
                ResetCameraView();
                break;
            default:
                break;
        }
    }
    void ResetCameraView() {
        if (viewfar > viewfarDefault)
        {
            viewfar -= viewfarChangeSpeed * 10 * Time.deltaTime;

            cinemachineCam.m_Lens.OrthographicSize = viewfar;
        }
        else cameraState = CameraState.idle;
    }
    void IncreaseViewFar(float viewfarPoint) {
        if (viewfar < viewfarPoint) { 
            viewfar += viewfarChangeSpeed * Time.deltaTime;
            cinemachineCam.m_Lens.OrthographicSize = viewfar;
        }
    }
    public void ShakeScene(float screenShakeValue) {
        cinemachineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = screenShakeValue;
        Invoke("ResetCinemachineCamNoise", 0.25f);
    }
    void ResetCinemachineCamNoise() {
        cinemachineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        ResetPlayerHurt();
    }
    public void PlayerHurt() {
        hurtPanel.SetActive(true);
    }
    public void ResetPlayerHurt() {
        hurtPanel.SetActive(false);
    }
    public void SetPolygonCamera(PolygonCollider2D polygonCollider2D) {
        cameraConfiner.m_BoundingShape2D = polygonCollider2D;
    }
}
