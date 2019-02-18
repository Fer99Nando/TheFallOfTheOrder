using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WeaponBoss : MonoBehaviour {

	private PlayerHealth playerHealth;
    BossPrueba bossprueba;

	void Start () 
	{
        bossprueba = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossPrueba>(); ;

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
	}
	
	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Player")
        {
            playerHealth.currentHp -= bossprueba.bonusEnemyStats;
        }
	}
}
