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
        player = GameObject.FindGameObjectWithTag("Player");

        if ((object)player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            

            targetPosition = player.transform.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(targetPosition);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 1);
           
        }

        trail.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "PlayerWeapon")
        {
            Debug.Log("AAuuuuuux");
            playerHealth.currentHp -= 3;
            Destroy(gameObject);
        }
        else if(other.tag != "Player" || other.tag != "PlayerWeapon")
        {
            Destroy(gameObject);
        }
    }
}
