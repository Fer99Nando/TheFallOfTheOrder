using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Weapon : MonoBehaviour {

	private PlayerHealth playerHealth;
    BossPrueba bossprueba;
    public int hitDamage = 5;
    public float virusDamage = 10;

	void Start () 
	{
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
	}
	
	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Player")
        {
            playerHealth.Damage(hitDamage);

            playerHealth.TakeVirus(virusDamage);
        }
	}
}
