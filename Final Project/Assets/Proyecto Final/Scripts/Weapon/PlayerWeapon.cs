using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour 
{
	public int damage = 10;

    public LayerMask layerMask;

    private void Update()
    {
        //MaskWeapon();
    }

    void OnTriggerEnter(Collider other)
	{
		Debug.Log("Toca");
		if (other.tag == "Enemy")
		{
            Debug.Log("enemy atravesado");
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
			enemy.TakeDamage (damage);
		}
	}

    /*public void MaskWeapon()
    {
        
        if (Physics.Raycast(transform.position, Vector3.forward, Mathf.Infinity, layerMask))

            Debug.Log("The ray hit the player");
    }*/
}
