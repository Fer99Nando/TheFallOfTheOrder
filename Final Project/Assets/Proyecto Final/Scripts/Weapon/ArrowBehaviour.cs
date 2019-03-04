using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private GameObject player;
    BossPrueba bossprueba;

    private Vector3 targetPosition;

    void Start()
    {
        bossprueba = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossPrueba>(); ;

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        player = GameObject.FindGameObjectWithTag("Player");

        targetPosition = player.transform.position - transform.position;
        Quaternion newRotation = Quaternion.LookRotation(targetPosition);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 1);
    }



    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("AAuuuuuux");
            playerHealth.currentHp -= bossprueba.bonusEnemyStats;
            Destroy(gameObject);
        }
    }
}
