using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour 
{
	
	public static bool GameIsPaused;
	
	public GameObject pauseMenuUI;
	public GameObject gameOver;

	public bool resumeButton;

    public Dropdown resolutionsDropsown;

    Resolution[] resolutions;

    //MUSICA
    public AudioMixer audioMixer;

	// Use this for initialization
	void Start () 
	{
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
		gameOver.SetActive (false);

 
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
	
	// Update is called once per frame
	void LateUpdate () 
	{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (GameIsPaused == true)
				{
					if (resumeButton == true )
					{
						Resume ();
					}
					else
					{
                        return;
					}
				}
				else
				{
				    Pause ();
				}
			}

	}

	public void Resume ()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
		Cursor.visible = false;
	}

	void Pause ()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
        Cursor.visible = true;
    } 
		public void MenuPrincipalScene ()
	{
		SceneManager.LoadScene ("Menu_Principal");
	}

    public void GameplayScene()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void MenuInicioScene()
    {
        SceneManager.LoadScene("Menu_Inicio");
    }

    public void Exit()
	{
		Application.Quit ();
	}

	    public void SetVolume ( float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
