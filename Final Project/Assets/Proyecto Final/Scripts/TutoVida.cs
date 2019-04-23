using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoVida : MonoBehaviour
{
    public GameObject tutVida;

    bool tut1;

    private void Awake()
    {
        tutVida.SetActive(false);
    }

    private void Update()
    {
        if(tut1)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            tutVida.SetActive(true);
            tut1 = true;
        }
    }

    public void Resumebutton1()
    {
        tut1 = false;
    }
}
