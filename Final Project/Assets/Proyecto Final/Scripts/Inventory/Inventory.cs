using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image[] inventory;
    public Sprite[] items;
    public GameObject[] spritePot;

    private int inventoryAmount;

    // Start is called before the first frame update
    void Start()
    {
        inventoryAmount = 0;
        spritePot[0].SetActive(false);
        spritePot[1].SetActive(false);
        spritePot[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventoryAmount < 3)
            {

                ItemsAmount();
            }
            else
            {
                Debug.Log("MAX ITEMS");
            }
        }*/
    }

    public void ItemsAmount()
    {
        inventoryAmount += 1;

        if ( inventoryAmount == 0)
        {
            spritePot[0].SetActive(false);
            spritePot[1].SetActive(false);
            spritePot[2].SetActive(false);
        }
        if( inventoryAmount == 1)
        {
            spritePot[0].SetActive(true);

        }
        if (inventoryAmount == 2)
        {
            spritePot[1].SetActive(true);
        }
        if (inventoryAmount == 3)
        {
            spritePot[2].SetActive(true);
        }
    }
}
