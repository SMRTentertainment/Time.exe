using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using System;

public class ScreenTransitionManager : MonoBehaviour
{
    [SerializeField] private CinemachineCamera camPC;
    [SerializeField] private CinemachineCamera camMonitor;
    [SerializeField] private CinemachineCamera camZoom;

    [SerializeField] private CanvasGroup fadeGroup;
    [SerializeField] private float fadeDuration = 1.5f;

    public void StartTransitionSequence(System.Action onComplete)
    {
        StartCoroutine(TransitionRoutine(onComplete));
    }

    private IEnumerator TransitionRoutine(System.Action onComplete)
    {
        SetPriority(camPC, 10);
        SetPriority(camMonitor, 20);
        yield return new WaitForSeconds(1f);


        SetPriority(camZoom, 30);
        yield return new WaitForSeconds(0.6f);

        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeGroup.alpha = timer / fadeDuration;
            yield return null;
        }
        fadeGroup.alpha = 1f;

        onComplete?.Invoke();
        yield return new WaitForSeconds(0.1f);
        
        
        timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeGroup.alpha = 1f - (timer / fadeDuration);
            yield return null;
        }
        fadeGroup.alpha = 0f;
    }

    public void ResetCameras()
    {
        SetPriority(camPC, 20);
        SetPriority(camMonitor, 10);
        SetPriority(camZoom, 5);
        if (fadeGroup != null) fadeGroup.alpha = 0f;
    }

    private void SetPriority(CinemachineCamera cameraComponent, int priorityValue)
    {
        if (cameraComponent != null)
        {
            cameraComponent.Priority.Value = priorityValue;
        }
    }
}

