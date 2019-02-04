using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRecollect : MonoBehaviour
{
    public GameObject LifePot;

    PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        LifePot.SetActive(false);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Aaa, me tocaste");

            Recollection();

            Destroy(gameObject);
        }
    }

    public void Recollection()
    {
        playerHealth.canHeal = true;
        LifePot.SetActive(true);
    }
    

}
