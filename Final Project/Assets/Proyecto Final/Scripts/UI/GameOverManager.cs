using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{

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
        gameOver.SetActive (true);
    }
}
