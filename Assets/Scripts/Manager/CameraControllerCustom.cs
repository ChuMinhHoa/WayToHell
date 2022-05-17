using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraControllerCustom : MonoBehaviour
{
    public static CameraControllerCustom instance;
    public CinemachineVirtualCamera cinemachineCam;
    private void Awake()
    {
        instance = this;
    }

    public void ShakeScene(float screenShakeValue) {
        cinemachineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = screenShakeValue;
        Invoke("ResetCinemachineCamNoise", 0.25f);
    }
    void ResetCinemachineCamNoise() {
        cinemachineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
}
