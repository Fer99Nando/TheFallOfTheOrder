using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour 
{
	public int damage = 10;

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Toca");
		if (other.tag == "Wall")
		{
            Debug.Log("enemy atravesado");
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
			enemy.TakeDamage (damage);
		}
	}
}
