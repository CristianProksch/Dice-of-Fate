using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManaSkill : PinComponentBase
{
    public int attackPower;
    public int manaNeeded;

    internal override void TriggerAction(IPinOwner owner)
    {
        if (owner.GetCurrentMana() >= manaNeeded)
        {
            owner.AddAttackPower(attackPower);
            owner.loseMana(manaNeeded);
        }
    }
}
