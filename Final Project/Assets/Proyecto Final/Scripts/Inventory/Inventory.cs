using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    PlayerBehaviour playerBehaviour;
    PlayerHealth playerHealth;

    public bool potionOn;

    public GameObject potionLife;
    public GameObject potionAntidoto;
    public GameObject potionMix;

    //public Image[] inventory;
    public Sprite[] spriteItems;
    public GameObject[] slotPot;
    public int[] slotType = { 3, 3, 3};


    public int inventoryAmount;

    // Start is called before the first frame update
    void Start()
    {
        potionLife.SetActive(false);
        potionAntidoto.SetActive(false);
        potionMix.SetActive(false);

        potionOn = false;

        playerHealth = GetComponent<PlayerHealth>();
        playerBehaviour = GetComponent<PlayerBehaviour>();

        inventoryAmount = 0;
        slotPot[0].SetActive(false);
        slotPot[1].SetActive(false);
        slotPot[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && !potionOn)
        {
            UsePotion(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !potionOn)
        {
            UsePotion(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !potionOn)
        {
            UsePotion(2);
        }
    }

    void UsePotion(int slot)
    {
        if(inventoryAmount >= 1)
        {
            inventoryAmount -= 1;

            if (slotType[slot] == 0)
            {
                Debug.Log("health");
                playerHealth.PotionHelath();

                potionOn = true;

                playerBehaviour.DrinkPotion();
                potionLife.SetActive(true);
            }
            if (slotType[slot] == 1)
            {
                Debug.Log("antidoto");
                playerHealth.PotionAntidoto();

                potionOn = true;

                playerBehaviour.DrinkPotion();
                potionAntidoto.SetActive(true);
            }
            if (slotType[slot] == 2)
            {
                Debug.Log("toditoenuno");
                playerHealth.PotionAllInOne();

                potionOn = true;

                playerBehaviour.DrinkPotion();
                potionMix.SetActive(true);
            }

            slotPot[slot].SetActive(false);
            slotType[slot] = 3;
        }
    }

    public void ItemsVida(int potion)
    {
        if (inventoryAmount < 3)
        {
            inventoryAmount += 1;
        }
        else
        {
            Debug.Log("MAX ITEMS");
        }

        for (int i = 0; i < slotType.Length; i ++)
        {
            if (slotType[i] == 3)
            {
                slotPot[i].SetActive(true);
                slotPot[i].GetComponent<Image>().sprite = spriteItems[potion];
                DetectPotion(potion, i);
                i = 3;
            }
        }

        Debug.Log(inventoryAmount);
    }

    void DetectPotion(int pot, int pos)
    {
        switch (pot)
        {
            case 0:
                slotType[pos] = pot;
                break;
            case 1:
                slotType[pos] = pot;
                break;
            case 2:
                slotType[pos] = pot;
                break;
        }
    }
}
