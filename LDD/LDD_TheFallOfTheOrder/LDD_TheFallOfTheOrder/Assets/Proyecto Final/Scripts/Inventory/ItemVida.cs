using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemVida : MonoBehaviour
{
    
    public int potion;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Aaa, me tocaste");
            Inventory inventory = col.gameObject.GetComponent<Inventory>();
            inventory.ItemsVida(potion);
            Destroy(this.gameObject);
        }
    }
}
