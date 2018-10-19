using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{

		
	}
	public void MenuPrincipalScene ()
	{
		SceneManager.LoadScene ("Menu_Principal");
	}
	public void GameplayScene ()
	{
		SceneManager.LoadScene ("Gameplay");
	}
	public void OptionsSnece ()
	{
		SceneManager.LoadScene ("Credits");
	}
	public void CreditsScene ()
	{
		SceneManager.LoadScene ("Options");
	}
	public void Exit()
	{
		Application.Quit ();
	}
}
