using System;
using UnityEngine;
using UnityEngine.Audio;

public class MenuUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject canvasPlay;
    [SerializeField] private GameObject canvasOptions;
    [SerializeField] private GameObject canvasExit;

    [SerializeField] private AudioMixer mainMixer;
    private void Awake()
    {
        HideAllMenus();
    }

    public void ShowMenu(string action)
    {
        canvasPlay.SetActive(false);
        canvasOptions.SetActive(false);
        canvasExit.SetActive(false);
        
        if (action == "EXIT")
        {
            canvasExit.SetActive(true);
            if (mainMixer != null) mainMixer.SetFloat("MasterPitch", 0.5f); 
        }
        else
        {
            if (mainMixer != null) mainMixer.SetFloat("MasterPitch", 1.0f);
            
            if (action == "PLAY") canvasPlay.SetActive(true);
            if (action == "OPTIONS") canvasOptions.SetActive(true);
        }

        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideAllMenus()
    {
        if (canvasPlay != null) canvasPlay.SetActive(false);
        if (canvasOptions != null) canvasOptions.SetActive(false);
        if (canvasExit != null) canvasExit.SetActive(false);
        if (mainMixer != null) mainMixer.SetFloat("MasterPitch", 1.0f);
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined; 
    }
}
