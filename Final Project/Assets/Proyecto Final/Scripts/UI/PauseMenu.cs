using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour 
{
	
	public static bool GameIsPaused = false;
	
	public GameObject pauseMenuUI;
	public bool resumeButton;

//MUSICA
	public AudioMixer audioMixer;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsPaused)
			{
				if (resumeButton == true )
				{
					Resume ();
				}
				else
				{

				}
			}
			else
			{
				Pause();
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
	} 
		public void MenuPrincipalScene ()
	{
		SceneManager.UnloadSceneAsync ("Gameplay");
		SceneManager.LoadScene ("Menu_Principal");
		
	}
	public void Exit()
	{
		Application.Quit ();
	}

	    public void SetVolume ( float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
