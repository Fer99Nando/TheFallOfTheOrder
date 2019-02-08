using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item3D : MonoBehaviour
{
    Inventory inventory;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Aaa, me tocaste");

            inventory.ItemsAmount();
            Destroy(this.gameObject);
        }
    }
}
