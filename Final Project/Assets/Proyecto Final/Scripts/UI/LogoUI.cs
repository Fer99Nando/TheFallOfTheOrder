using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LogoUI : MonoBehaviour 
{
		public GameObject fade; 	

	public Animator anim;

	private float timeCounter;

	void Awake()
	{
		anim.SetBool("Fade", false);
	}

	void Update () 
	{
		timeCounter += Time.deltaTime;
		LogoToMenuScene();	
	}

	public void LogoToMenuScene ()
	{
		if (timeCounter >= 7)
		{
			SceneManager.LoadScene ("Menu_Principal");
		}

		if(Input.anyKeyDown && timeCounter <= 7)
		{
			StartCoroutine(Fading());
		}
	}

	IEnumerator Fading()
	{
		anim.SetBool("Fade", true);
		yield return new WaitForSeconds (1.0f);
		SceneManager.LoadScene("Menu_Principal");
	}
}
