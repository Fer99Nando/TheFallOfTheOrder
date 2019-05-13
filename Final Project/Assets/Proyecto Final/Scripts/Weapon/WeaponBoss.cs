using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WeaponBoss : MonoBehaviour {

	private PlayerHealth playerHealth;
    BossPrueba bossprueba;

    BoxCollider boxCol;

    void Start () 
	{
        bossprueba = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossPrueba>(); ;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        boxCol = GetComponent<BoxCollider>();

        boxCol.enabled = false;
    }
	
	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Player")
        {
            playerHealth.Damage(bossprueba.bonusEnemyStats);
            boxCol.enabled = false;
        }
	}

    public void BoxEnabled()
    {
        boxCol.enabled = true;
    }

    public void BoxDisabled()
    {
        boxCol.enabled = false;
    }
}
