using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedBoss : MonoBehaviour
{
    public GameObject bloqueNoPass;

    private void Start()
    {
        bloqueNoPass.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            bloqueNoPass.SetActive(true);
        }
    }
}
