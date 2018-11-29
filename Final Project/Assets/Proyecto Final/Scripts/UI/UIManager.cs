using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour 
{
	public GameObject fade; 	

	public Animator anim;

	private float timeCounter;

	void Awake()
	{
		anim.SetBool("Fade", false);
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeCounter += Time.deltaTime;
	}
	public void MenuPrincipalScene ()
	{
		SceneManager.LoadScene ("Menu_Principal");
	}

	public void ButtonExitToMenu ()
	{
		StartCoroutine(Fading());
		SceneManager.LoadScene ("Menu_Principal");	
	}

	public void GameplayScene ()
	{
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
		yield return new WaitForSeconds (1.0f);
		SceneManager.LoadScene("Menu_Principal");
	}
}
