using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectorBoss : MonoBehaviour
{
    BossPrueba bossStart;
    public GameObject vidaBoss;

    private void Awake()
    {
        vidaBoss.SetActive (false);
        bossStart = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossPrueba>();
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == ("Player"))
        {
            vidaBoss.SetActive(true);
            
            bossStart.SetIdle();
        }
    }
}
