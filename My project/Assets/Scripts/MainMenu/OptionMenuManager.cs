using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenuManager : MonoBehaviour
{
    [Header("Audio Configuration")]
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private Toggle toggleMute;

    [Header("Video Configuration")]
    [SerializeField] private Toggle toggleFullScreen;
    [SerializeField] private TMP_Dropdown dropdownResolutions;

    [Header("Return Button")]
    [SerializeField] private Button buttonDisconnect;
    
    
    private Resolution[] resolutions;
    private float previousMasterVolume = 0.4f; 

    void Start()
    {
        Screen.fullScreen = true;
        if (toggleFullScreen != null) toggleFullScreen.isOn = true;

        sliderMaster.value = 0.4f;
        sliderMusic.value = 0.4f;
        sliderSFX.value = 0.4f;
        SetMixerVolume("MasterVol", 0.4f);
        SetMixerVolume("MusicVol", 0.4f);
        SetMixerVolume("SFXVol", 0.4f);
        
        BuildResolutionDropdown();

        sliderMaster.onValueChanged.AddListener(delegate { OnMasterSliderChanged(); });
        sliderMusic.onValueChanged.AddListener(delegate { SetMixerVolume("MusicVol", sliderMusic.value); });
        sliderSFX.onValueChanged.AddListener(delegate { SetMixerVolume("SFXVol", sliderSFX.value); });
        
        toggleMute.onValueChanged.AddListener(delegate { OnMuteToggled(toggleMute.isOn); });
        toggleFullScreen.onValueChanged.AddListener(delegate { OnFullScreenToggled(toggleFullScreen.isOn); });
        
        if (dropdownResolutions != null)
        {
            dropdownResolutions.onValueChanged.AddListener(delegate { OnResolutionChanged(dropdownResolutions.value); });
        }
        
        buttonDisconnect.onClick.AddListener(delegate {
            MenuManager mainManager = FindFirstObjectByType<MenuManager>();
            if (mainManager != null)
            {
                mainManager.Invoke("DisconnectUSB", 0f); 
            }
        });
    }

    private void SetMixerVolume(string parameterName, float sliderValue)
    {
        float dBValue = Mathf.Log10(Mathf.Max(0.0001f, sliderValue)) * 20f;
        mainMixer.SetFloat(parameterName, dBValue);
    }

    private void OnMasterSliderChanged()
    {
        if (!toggleMute.isOn)
        {
            SetMixerVolume("MasterVol", sliderMaster.value);
        }
    }

    private void OnMuteToggled(bool isMuted)
    {
        if (isMuted)
        {
            previousMasterVolume = sliderMaster.value; 
            SetMixerVolume("MasterVol", 0.0001f);
        }
        else
        {
            SetMixerVolume("MasterVol", previousMasterVolume);
        }
    }

    private void OnFullScreenToggled(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    private void BuildResolutionDropdown()
    {
        if (dropdownResolutions == null) return;

        resolutions = Screen.resolutions;

        dropdownResolutions.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        dropdownResolutions.AddOptions(options);
        dropdownResolutions.value = currentResolutionIndex;
        dropdownResolutions.RefreshShownValue();
    }
    
    private void OnResolutionChanged(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
