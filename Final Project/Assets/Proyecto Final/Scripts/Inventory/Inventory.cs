using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    PlayerHealth playerHealth;
    Animator anim;

    //public Image[] inventory;
    public Sprite[] spriteItems;
    public GameObject[] slotPot;
    public int[] slotType = { 3, 3, 3};


    private int inventoryAmount;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        inventoryAmount = 0;
        slotPot[0].SetActive(false);
        slotPot[1].SetActive(false);
        slotPot[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            UsePotion(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UsePotion(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
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
            }
            if (slotType[slot] == 1)
            {
                    Debug.Log("antidoto");
                    playerHealth.PotionAntidoto();
            }
            if (slotType[slot] == 2)
            {
                    Debug.Log("toditoenuno");
                    playerHealth.PotionAllInOne();
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
