using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Weapon : MonoBehaviour {

	private PlayerHealth playerHealth;
	//private PlayerHealth virusHealth;

	// Use this for initialization
	[SerializeField] private Transform targetTransform;
	void Start () 
	{
		targetTransform = GameObject.FindGameObjectWithTag("Player").transform;

        playerHealth = targetTransform.GetComponent<PlayerHealth>();
		//virusHealth = targetTransform.GetComponent<PlayerHealth>();
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
            playerHealth.currentHp -= 10f;
			//virusHealth.TakeVirus(vDamage);
					
		}
	}
}
