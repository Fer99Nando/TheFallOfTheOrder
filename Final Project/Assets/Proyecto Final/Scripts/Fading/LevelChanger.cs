using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    public Animator animator;

    private int levelToLoad;

    private float count;

    //private float count;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        

        

        count++;

        if (count >=  480)
        {
            FadeToNextLevel();
        }
        

        if (Input.GetMouseButtonDown(0))
        {
            FadeToNextLevel();
        }
    }

    public void FadeToNextLevel ()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToLevel (int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete ()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
