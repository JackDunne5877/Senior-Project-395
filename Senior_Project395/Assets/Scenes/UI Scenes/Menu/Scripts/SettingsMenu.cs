using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    public void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> stringOptions = new List<string>();

        int resIdx = 0;

        for(int i = 0 ; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            stringOptions.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width 
                && resolutions[i].height == Screen.currentResolution.height)
            {
                resIdx = i;
            }
                
        }
        resolutionDropdown.AddOptions(stringOptions);
        resolutionDropdown.value = resIdx;
        resolutionDropdown.RefreshShownValue();
    }

    public void setResolution(int resIdx)
    {
        Resolution res = resolutions[resIdx];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("volume", value);
    }

    public void SetQuality(int settingIdx)
    {
        QualitySettings.SetQualityLevel(settingIdx);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
