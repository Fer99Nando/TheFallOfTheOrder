using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Weapon : MonoBehaviour {

	private PlayerHealth playerHealth;
    BossPrueba bossprueba;

	void Start () 
	{
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
	}
	
	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Player")
        {
            playerHealth.currentHp -= 5;

            playerHealth.TakeVirus();
        }
	}
}
