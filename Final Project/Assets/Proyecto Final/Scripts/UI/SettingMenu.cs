using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingMenu : MonoBehaviour
{
    //public Slider volumeSlider;
    public AudioMixer audioMixer;
    //public AudioMixer fxMusic;

   // public Slider volume;
   // public Slider fxVolume;

    public Dropdown resolutionsDropsown;

    Resolution[] resolutions;

    void Start ()
    {
       // fxVolume.value = PlayerPrefs.GetFloat("MusicVolume");
       // volume.value = PlayerPrefs.GetFloat("FxVolume");

        resolutions = Screen.resolutions;

        resolutionsDropsown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionsDropsown.AddOptions(options);
        resolutionsDropsown.value = currentResolutionIndex;
        resolutionsDropsown.RefreshShownValue();
    }

    private void Update()
    {
        // = volume.value;
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume ( float volume)
    {
        //volume = volumeSlider.value;

        audioMixer.SetFloat("volume", volume);
        //fxMusic.SetFloat("volume", volume);
    }
    
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen ( bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
