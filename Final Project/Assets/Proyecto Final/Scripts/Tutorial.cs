using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
    public GameObject general;
    public Image general1;
    public Text general2;

    public float timeCount;
    public bool timeOn;

    // Start is called before the first frame update
    void Start()
    {
        timeOn = false;
        general.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeOn)
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
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Toco El Collider");
            general.SetActive(true);
            timeOn = true;
            this.gameObject.SetActive(false);
        }
    }
}
