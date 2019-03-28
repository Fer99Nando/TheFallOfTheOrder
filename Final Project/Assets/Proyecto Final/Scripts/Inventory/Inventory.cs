using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    PlayerBehaviour playerBehaviour;
    PlayerHealth playerHealth;
    Animator anim;

    private bool timeReg;
    private float tomandoPoti;

    //public Image[] inventory;
    public Sprite[] spriteItems;
    public GameObject[] slotPot;
    public int[] slotType = { 3, 3, 3};


    private int inventoryAmount;

    // Start is called before the first frame update
    void Start()
    {
        timeReg = false;
        anim = GetComponent<Animator>();
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
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            timeReg = true;
            UsePotion(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            timeReg = true;
            UsePotion(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            timeReg = true;
            UsePotion(2);
        }

        if(timeReg)
        {
            tomandoPoti += Time.deltaTime;
        }
    }

    void UsePotion(int slot)
    {
        if(inventoryAmount >= 1)
        {
            playerBehaviour.canMove = false;
            anim.SetBool("PotiOn", true);
            inventoryAmount -= 1;

            if (slotType[slot] == 0)
            {
                if (tomandoPoti >= 1)
                {
                    Debug.Log("health");
                    playerHealth.PotionHelath();
                    tomandoPoti = 0;
                    timeReg = false;
                }
            }
            if (slotType[slot] == 1)
            {
                if (tomandoPoti >= 1)
                {
                    Debug.Log("antidoto");
                    playerHealth.PotionAntidoto();
                    tomandoPoti = 0;
                    timeReg = false;
                }
            }
            if (slotType[slot] == 2)
            {
                if (tomandoPoti >= 1)
                {
                    Debug.Log("toditoenuno");
                    playerHealth.PotionAllInOne();
                    tomandoPoti = 0;
                    timeReg = false;
                }
            }

            slotPot[slot].SetActive(false);
            slotType[slot] = 3;
        }
    }

    public void PotiTerminada()
    {
        playerBehaviour.canMove = true;
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
