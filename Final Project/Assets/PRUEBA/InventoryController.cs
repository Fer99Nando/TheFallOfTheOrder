using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    public PlayerWeaponController playerWeaponController;

    public Item axe;

    void Start()
    {
        playerWeaponController = GetComponent<PlayerWeaponController>();
        List<BaseStats> axeStats = new List<BaseStats>();
        axeStats.Add(new BaseStats(6, "Power", "Your power level."));
        axe = new Item(axeStats, "axe");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            playerWeaponController.EquipWeapon(axe);
        }
    }
}
