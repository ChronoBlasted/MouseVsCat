using BaseTemplate.Behaviours;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField] List<CinemachineVirtualCamera> cameras;
    CinemachineVirtualCamera currentCamera;
    [SerializeField] string firstCamera;

    Coroutine shakeCoroutine;

    public CinemachineVirtualCamera CurrentCamera { get => currentCamera; }

    public void Init()
    {
        GameManager.Instance.OnGameStateChanged += HandleStateChange;

        currentCamera = cameras.FirstOrDefault(cam => cam.name == firstCamera);
        currentCamera.Priority = 1;
    }

    public void SwitchCamera(string cameraName)
    {
        if (currentCamera != null)
        {
            currentCamera.Priority = 0;
        }

        currentCamera = cameras.FirstOrDefault(cam => cam.name == cameraName);

        if (currentCamera != null)
        {
            currentCamera.Priority = 1;
        }
    }

    public void ShakeCamera(float intensity = 4, float duration = 2)
    {
        StopShake();

        var currentShake = currentCamera.GetComponent<CinemachineShake>();

        if (currentShake != null)
        {
            shakeCoroutine = StartCoroutine(currentShake.ShakeCamera(intensity, duration));
        }
    }

    public void StopShake()
    {
        var currentShake = currentCamera.GetComponent<CinemachineShake>();

        if (currentShake != null)
        {
            currentShake.StopShake();

            if (shakeCoroutine != null)
            {
                StopCoroutine(shakeCoroutine);
                shakeCoroutine = null;
            }
        }
    }

    void HandleStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.MENU:
                SwitchCamera("MenuCamera");
                break;
            case GameState.GAME:
                SwitchCamera("GameCamera");
                break;
            case GameState.PAUSE:
                break;
            case GameState.END:
                break;
            case GameState.WAIT:
                break;
            default:
                break;
        }
    }
}
