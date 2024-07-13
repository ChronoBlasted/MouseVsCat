using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;

    Tweener _shakeTween;
    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    private void Awake()
    {
        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public IEnumerator ShakeCamera(float intensity, float duration)
    {
        if (_shakeTween.IsActive()) _shakeTween.Kill();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        yield return new WaitForSeconds(duration);

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
    }

    public void StopShake()
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
    }
}
