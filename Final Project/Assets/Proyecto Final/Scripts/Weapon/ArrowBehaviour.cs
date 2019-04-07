using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private GameObject player;

    public ParticleSystem trail;

    //public ParticleSystem trail;
    private Vector3 targetPosition;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        player = GameObject.FindGameObjectWithTag("Player");

        targetPosition = player.transform.position - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(targetPosition);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 1);

        trail.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("AAuuuuuux");
            playerHealth.currentHp -= 10;
            Destroy(gameObject);
        }

    }

    /*private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject)
        {
            Destroy(gameObject);
        }
    }*/


}
