using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    PlayerHealth playerHealth;

    //public Image[] inventory;
    public Sprite[] spriteItems;
    public GameObject[] slotPot;
    public GameObject[] potScene;

    private int inventoryAmount;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        inventoryAmount = 0;
        slotPot[0].SetActive(false);
        slotPot[1].SetActive(false);
        slotPot[2].SetActive(false);
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

        }*/
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            UsePotion();
        }
    }

    void UsePotion()
    {
        //spritePot[pot].SetActive(false);
        if(inventoryAmount >= 1 && spriteItems[0] == slotPot[0])
        {
            inventoryAmount -= 1;
            playerHealth.PotionHelath();
            slotPot[0].SetActive(false);
        }
        /*else
        {
            MovePotions(pot);
        }*/
    }

    void MovePotions(int pot)
    {
        //spritePot[pot+1]
    }

    public void ItemsVida(int potion)
    {
        inventoryAmount += 1;
        Debug.Log(inventoryAmount);
        if ( inventoryAmount == 0)
        {
            slotPot[0].SetActive(false);
            slotPot[1].SetActive(false);
            slotPot[2].SetActive(false);
        }
        else if( inventoryAmount == 1 )
        {
            slotPot[0].SetActive(true);
            slotPot[0].GetComponent<Image>().sprite = spriteItems[potion];

        }
        else if (inventoryAmount == 2)
        {
            slotPot[1].SetActive(true);
            slotPot[1].GetComponent<Image>().sprite = spriteItems[potion];
        }
        else if (inventoryAmount == 3)
        {
            slotPot[2].SetActive(true);
            slotPot[2].GetComponent<Image>().sprite = spriteItems[potion];
        }
        else if (inventoryAmount > 3)
        {
            Debug.Log("MAX ITEMS");
        }
    }
}
