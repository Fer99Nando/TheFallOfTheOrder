using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutoVida : MonoBehaviour
{
    public GameObject general;

    public GameObject vida;

    public Image general1;
    public Text general2;
    
    public GameObject tutAntidoto;

    public GameObject antidoto;

    public Image antidoto1;
    public Text antidoto2;

    public GameObject tutTotal;

    public GameObject total;
    
    public Image total1;
    public Text total2;

    public float timeCount;
    public bool timeOn;
    public bool timeOnA;
    public bool timeOnV;

    private void Awake()
    {
        timeOn = false;
        timeOnA = false;
        timeOnV = false;

        general.SetActive(false);
        antidoto.SetActive(false);
        total.SetActive(false);
    }
    
    void Update()
    {
        if (timeOn || timeOnA || timeOnV)
        {
            timeCount += Time.deltaTime;
        }

        if (timeCount >= 2)
        {
            general1.DOFade(0, 2).OnComplete(DestroyTitle);
            general2.DOFade(0, 2);
            timeOn = false;
            timeCount = 0;
        }
    }

    public void DestroyTitle()
    {
        Destroy(general1);
        Destroy(general2);
        Destroy(general);
        Destroy(vida);
    }

    public void DestroyTitle1()
    {
        Destroy(antidoto);
        Destroy(antidoto1);
        Destroy(antidoto2);
        Destroy(tutAntidoto);
    }

    public void DestroyTitle2()
    {
        Destroy(tutTotal);
        Destroy(total1);
        Destroy(total2);
        Destroy(total);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(vida.tag == "Player")
        {
            Debug.Log("Toco El Collider");
            general.SetActive(true);
            timeOn = true;
            vida.SetActive(false);
        }

        if (antidoto.tag == "Player")
        {
            Debug.Log("Toco El Collider");
            tutAntidoto.SetActive(true);
            timeOnA = true;
            antidoto.SetActive(false);
        }

        if (total.tag == "Player")
        {
            Debug.Log("Toco El Collider");
            tutTotal.SetActive(true);
            timeOnV = true;
            total.SetActive(false);
        }
    }
}
