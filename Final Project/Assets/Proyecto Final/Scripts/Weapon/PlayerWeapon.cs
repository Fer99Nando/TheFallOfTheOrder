using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {


	public int damage = 10;

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.tag);
		if (other.gameObject.tag == "Enemy")
		{
			Debug.Log(other.tag);
			EnemyHealth enemy = other.GetComponent<EnemyHealth>();
			enemy.TakeDamage (damage);
		}
	}
}
