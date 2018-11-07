using System.Collections.Generic;


public interface IWeapon
{
    List<BaseStats> Stats { get; set; }
    void PerformAttack();
}
