using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject gameOver;

    public static GameOverManager gameOverManager;

	// Use this for initialization
	void Start ()
    {
        gameOverManager = this;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void CallGameOver ()
    {
        Pause ();
    }

    	void Pause ()
	{
        gameOver.SetActive (true);
		Time.timeScale = 0f;
		GameIsPaused = true;
        Cursor.visible = true;
	} 
    
}
