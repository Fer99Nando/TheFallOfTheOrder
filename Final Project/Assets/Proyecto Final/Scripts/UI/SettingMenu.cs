using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public float vol_1;
    private float vol_2;
    private float vol_3;
    public Slider master;
    public Slider effects;
    public Slider music;

    public Dropdown resolutionsDropsown;

    Resolution[] resolutions;

    void Start ()
    {
        vol_1 = PlayerPrefs.GetFloat("Master_Vol");
        master.value = vol_1;
        Debug.Log(master.value);
        audioMixer.SetFloat("Master", vol_1);

        vol_2 = PlayerPrefs.GetFloat("Effects_Vol");
        effects.value = vol_2;
        audioMixer.SetFloat("Effects", vol_2);

        vol_3 = PlayerPrefs.GetFloat("Music_Vol");
        music.value = vol_3;
        audioMixer.SetFloat("Music", vol_3);

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

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolumeMaster ( float volume_1)
    {
        vol_1 = volume_1;
        audioMixer.SetFloat("Master", volume_1);
        SaveVolume_Master();
    }

    public void SetVolumeEffects ( float volume_2)
    {
        vol_2 = volume_2;
        audioMixer.SetFloat("Effects", volume_2);
        SaveVolume_Effects();
    }

    public void SetVolumeMusic ( float volume_3)
    {
        vol_3 = volume_3;
        audioMixer.SetFloat("Music", volume_3);
        SaveVolume_Music();
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen ( bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    #region SAVE
    void SaveVolume_Master()
    {
        PlayerPrefs.SetFloat("Master_Vol", vol_1);
        Debug.Log(vol_1);
        //PlayerPrefs.Save();
    }

    void SaveVolume_Effects()
    {
        PlayerPrefs.SetFloat("Effects_Vol", vol_2);
    }

    void SaveVolume_Music()
    {
        PlayerPrefs.SetFloat("Music_Vol", vol_3);
    }
    #endregion
}