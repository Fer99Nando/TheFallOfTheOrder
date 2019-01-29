using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRecollect : MonoBehaviour
{
    public GameObject LifePot;

    private void Start()
    {
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
        if (LifePot == false)
        {
            LifePot.SetActive(true);
        }
    }
}
