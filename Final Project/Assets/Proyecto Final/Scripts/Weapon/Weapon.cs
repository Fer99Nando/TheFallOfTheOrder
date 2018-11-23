using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Weapon : MonoBehaviour {

	private PlayerHealth playerHealth;
	private PlayerHealth virusHealth;

	public int damage = 10;
	// Use this for initialization
	[SerializeField] private Transform targetTransform;
	void Start () 
	{
		targetTransform = GameObject.FindGameObjectWithTag("Player").transform;

        playerHealth = targetTransform.GetComponent<PlayerHealth>();
		virusHealth = targetTransform.GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			playerHealth = other.GetComponent<PlayerHealth>();
			playerHealth.TakeDamage (damage);
			virusHealth.TakeVirus(damage);			
		}
	}
}
