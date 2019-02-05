using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    public GameObject [] items;

    bool [] empty;

    // Start is called before the first frame update
    void Start()
    {
        items = new GameObject[3];

        items = GameObject.FindGameObjectsWithTag("Item");

        empty = new bool[3];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Inventory()
    {
        
    }
}
