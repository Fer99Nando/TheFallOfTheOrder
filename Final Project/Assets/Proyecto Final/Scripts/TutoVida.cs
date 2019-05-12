using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutoVida : MonoBehaviour
{
    public GameObject vida;

    public GameObject antidoto;

    public GameObject total;

    public float timeCount;
    public bool timeOn;
    public bool timeOnA;
    public bool timeOnV;

    private void Awake()
    {
        timeOn = false;
        timeOnA = false;
        timeOnV = false;

        vida.SetActive(false);
        antidoto.SetActive(false);
        total.SetActive(false);
    }
    
    void Update()
    {
        if (timeOn || timeOnA || timeOnV)
        {
            timeCount += Time.deltaTime;

            if (timeCount >= 3 && timeOn)
            {
                timeOn = false;
                timeCount = 0;
                DestroyTitle2();
            }

            if (timeCount >= 3 && timeOnA)
            {
                timeOnA = false;
                timeCount = 0;
                DestroyTitle1();
            }

            if (timeCount >= 3 && timeOnV)
            {
                timeOnV = false;
                timeCount = 0;
                DestroyTitle();
            }
        }
    }

    public void DestroyTitle()
    {
        Destroy(vida);
    }

    public void DestroyTitle1()
    {
        Destroy(antidoto);
    }

    public void DestroyTitle2()
    {
        Destroy(total);
    }
}
