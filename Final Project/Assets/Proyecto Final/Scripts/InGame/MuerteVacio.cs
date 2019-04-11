using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteVacio : MonoBehaviour
{
    PlayerHealth player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("player"))
        {
            player.DieAcabado();
        }
    }
}
