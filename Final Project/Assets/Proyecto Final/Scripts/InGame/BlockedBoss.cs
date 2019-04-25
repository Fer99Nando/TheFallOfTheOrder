using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedBoss : MonoBehaviour
{
    public GameObject bloqueNoPass;

    public AudioSource musicaBossEpic;

    private void Start()
    {
        bloqueNoPass.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            musicaBossEpic.Play();
            bloqueNoPass.SetActive(true);
        }
    }
}
