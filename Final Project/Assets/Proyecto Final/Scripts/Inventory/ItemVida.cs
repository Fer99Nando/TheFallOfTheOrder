using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemVida : MonoBehaviour
{
    public GameObject pot;
    public int potion;
    public Inventory inventory;

    public void Start()
    {
        inventory = inventory.GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (inventory.inventoryAmount < 3)
            {
                Debug.Log("Aaa, me tocaste");
                inventory.ItemsVida(potion);
                Destroy(pot);
            }
        }
    }
}
