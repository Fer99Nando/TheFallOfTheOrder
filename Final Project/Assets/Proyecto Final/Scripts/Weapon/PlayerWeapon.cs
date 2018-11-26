using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerWeapon : MonoBehaviour {

	private EnemyHealth enemyHealth;

	public int damage = 10;
	// Use this for initialization
	[SerializeField] private Transform targetTransform;
	void Start () 
	{
		targetTransform = GameObject.FindGameObjectWithTag("Enemy").transform;

        enemyHealth = targetTransform.GetComponent<EnemyHealth>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			enemyHealth.TakeDamage (damage);			
		}
	}
}
