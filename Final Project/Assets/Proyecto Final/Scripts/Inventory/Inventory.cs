using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //public Image[] inventory;
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

        }*/
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            UsePotion(0);
        }
    }

    void UsePotion(int pot){
        spritePot[pot].SetActive(false);
        if(inventoryAmount <= 1){
            inventoryAmount = 0;
        }else{
            MovePotions(pot);
        }
    }

    void MovePotions(int pot){
        //spritePot[pot+1]
    }

    public void ItemsVida(int potion)
    {
        inventoryAmount += 1;
        Debug.Log(inventoryAmount);
        if ( inventoryAmount == 0)
        {
            spritePot[0].SetActive(false);
            spritePot[1].SetActive(false);
            spritePot[2].SetActive(false);
        }
        else if( inventoryAmount == 1)
        {
            spritePot[0].SetActive(true);
            spritePot[0].GetComponent<Image>().sprite = items[potion];

        }
        else if (inventoryAmount == 2)
        {
            spritePot[1].SetActive(true);
            spritePot[1].GetComponent<Image>().sprite = items[potion];
        }
        else if (inventoryAmount == 3)
        {
            spritePot[2].SetActive(true);
            spritePot[2].GetComponent<Image>().sprite = items[potion];
        }
        else if (inventoryAmount > 3)
        {
            Debug.Log("MAX ITEMS");
        }
    }

    public void ItemsAntidoto()
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
        else
        {
            Debug.Log("MAX ITEMS");
        }
    }

    public void ItemsBoth()
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
        else
        {
            Debug.Log("MAX ITEMS");
        }
    }
}
