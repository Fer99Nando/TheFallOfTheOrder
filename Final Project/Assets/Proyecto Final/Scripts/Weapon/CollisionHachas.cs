using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHachas : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public MeshCollider este;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("AAuuuuuux");
            playerHealth.Damage(20);
        }
    }
}
