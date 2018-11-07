using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public List<BaseStats> stats = new List<BaseStats>();

    private void Start()
    {
        stats.Add(new BaseStats(4, "Power", "Your power level."));
        stats.Add(new BaseStats(2, "Vitality", "Your vitality level."));
        /* stats[0].AddStatBonus(new StatBonus(5)); //Ejemplo de aumentar stats */

        //Debug.Log(stats[0].GetCalculatedStatValue());
    }

    public void AddStatBonus(List<BaseStats> statBonuses)
    {
        foreach (BaseStats statBonus in statBonuses)
        {
            stats.Find(x=> x.StatName == statBonus.StatName).AddStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }

    public void RemoveStatBonus(List<BaseStats> statBonuses)
    {
        foreach (BaseStats statBonus in statBonuses)
        {
            stats.Find(x => x.StatName == statBonus.StatName).RemoveStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }
}
