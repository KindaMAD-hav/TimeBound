using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingMenuManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown ResDropDown;
    [SerializeField] Toggle FullScreenToggle;
    [SerializeField] Toggle VSyncToggle;
    [SerializeField] Slider MusicSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] Slider NarratorSlider;

    Resolution[] AllResolutions;
    bool IsFullScreen;
    int SelectedResolution;

    List<Resolution> SelectedResolutionList = new List<Resolution>();

    // Start is called before the first frame update
    void Start()
    {
        IsFullScreen = Screen.fullScreen; // Use current fullscreen state
        AllResolutions = Screen.resolutions;
        MusicSlider.value = AudioData.Instance.musicVolume;
        SFXSlider.value = AudioData.Instance.sfxVolume;
        NarratorSlider.value = AudioData.Instance.narratorVolume;

        List<string> resolutionStringList = new List<string>();
        string newRes;
        int currentResolutionIndex = 0;

        for (int i = 0; i < AllResolutions.Length; i++)
        {
            Resolution res = AllResolutions[i];
            newRes = res.width + " x " + res.height;

            if (!resolutionStringList.Contains(newRes))
            {
                resolutionStringList.Add(newRes);
                SelectedResolutionList.Add(res);
            }

            // Find index of current resolution
            if (res.width == Screen.currentResolution.width &&
                res.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = resolutionStringList.IndexOf(newRes);
            }
        }

        ResDropDown.ClearOptions();
        ResDropDown.AddOptions(resolutionStringList);
        ResDropDown.value = currentResolutionIndex;
        ResDropDown.RefreshShownValue();

        // Sync fullscreen toggle
        FullScreenToggle.isOn = IsFullScreen;

        // Sync VSync toggle
        VSyncToggle.isOn = QualitySettings.vSyncCount > 0;
    }

    public void ChangeMusicVolume()
    {
        AudioData.Instance.musicVolume = MusicSlider.value;
    }

    public void ChangeSFXVolume()
    {
        AudioData.Instance.sfxVolume = SFXSlider.value;
    }
    public void ChangeNarratorVolume()
    {
        AudioData.Instance.narratorVolume = NarratorSlider.value;
    }

    public void ChangeResolution()
    {
        SelectedResolution = ResDropDown.value;
        Screen.SetResolution(
            SelectedResolutionList[SelectedResolution].width,
            SelectedResolutionList[SelectedResolution].height,
            IsFullScreen
        );
    }

    public void ChangeFullScreen()
    {
        IsFullScreen = FullScreenToggle.isOn;
        Screen.SetResolution(
            SelectedResolutionList[SelectedResolution].width,
            SelectedResolutionList[SelectedResolution].height,
            IsFullScreen
        );
    }

    public void ChangeVSync()
    {
        if (VSyncToggle.isOn)
            QualitySettings.vSyncCount = 1; 
        else
            QualitySettings.vSyncCount = 0; 
    }

}
