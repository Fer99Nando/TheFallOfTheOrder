using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectorBoss : MonoBehaviour
{
    public GameObject vidaBoss;

    private void Awake()
    {
        vidaBoss.SetActive (false);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == ("Player"))
        {
            vidaBoss.SetActive(true);
            BossPrueba bossStart = col.GetComponent<BossPrueba>();
            bossStart.BecomeBoss();
        }
    }
}
