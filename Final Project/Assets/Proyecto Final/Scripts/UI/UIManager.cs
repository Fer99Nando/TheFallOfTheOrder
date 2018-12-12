using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour 
{
	public GameObject fade;
	public GameObject fadeLayer;	

	public Animator anim;

	private float timeCounter;

	void Awake()
	{
		anim.SetBool("Fade", false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeCounter += Time.deltaTime;
		if (timeCounter >= 1) fadeLayer.SetActive (false);
	}
	public void MenuPrincipalScene ()
	{
        StartCoroutine(Fading());
        SceneManager.LoadScene ("Menu_Principal");
	}

	public void ButtonExitToMenu ()
	{
		StartCoroutine(Fading());
		SceneManager.LoadScene ("Menu_Principal");	
	}

	public void GameplayScene ()
	{
		fadeLayer.SetActive (true);
		StartCoroutine(Fading());
		SceneManager.LoadScene ("Gameplay");
	}

	public void OptionsSnece ()
	{
		StartCoroutine(Fading());
		SceneManager.LoadScene ("Options");
	}

	public void CreditsScene ()
	{
		StartCoroutine(Fading());
		SceneManager.LoadScene ("Credits");
	}

	public void Exit()
	{
		Application.Quit ();
	}

	IEnumerator Fading()
	{
		anim.SetBool("Fade", true);
		timeCounter = 0;
		yield return new WaitForSeconds (1.0f);
		SceneManager.LoadScene("Menu_Principal");
	}
}
