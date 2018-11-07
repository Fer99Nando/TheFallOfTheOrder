using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Axe : MonoBehaviour, IWeapon
{

    public List<BaseStats> Stats { get; set; }

    public void PerformAttack()
    {
        Debug.Log("Axe attack!");
    }

}
