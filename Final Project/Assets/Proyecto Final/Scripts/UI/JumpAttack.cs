using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JumpAttack : MonoBehaviour
{

    private PlayerHealth playerHealth;
    BossPrueba bossprueba;

    BoxCollider boxCol;

    private GameObject particlePref;
    public GameObject particlePrefP;
    public GameObject SpawnPart;

    //private bool lavaSuelo;

    void Start()
    {
        bossprueba = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossPrueba>(); ;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        boxCol = GetComponent<BoxCollider>();

        boxCol.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerHealth.Damage(bossprueba.bonusEnemyStats);
            boxCol.enabled = false;
        }
    }

    public void BoxEnabled()
    {
        boxCol.enabled = true;
    }

    public void BoxDisabled()
    {
        boxCol.enabled = false;
    }

    public void ParticlesLava()
    {
        particlePref = Instantiate(particlePrefP, SpawnPart.transform.position, SpawnPart.transform.rotation);
    }
}