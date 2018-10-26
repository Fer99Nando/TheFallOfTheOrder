using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour 
{
	public static bool Cargando;

	public void Cargar ()
	{
		if (PlayerPrefs.GetFloat("Player x") != null && PlayerPrefs.GetFloat("Player z") != null)
		{
		SceneManager.LoadScene ("Gameplay");
		}
	}

	public void NoCargar ()
	{
		Cargando = false;
		SceneManager.LoadScene ("Gameplay");
	}
}
