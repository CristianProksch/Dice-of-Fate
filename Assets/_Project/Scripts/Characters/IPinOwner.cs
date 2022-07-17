using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPinOwner
{
    public virtual void AddAttackPower(int amount)
    {

    }

    public virtual void AddArmourPower(int amount)
    {

    }

    void AddMana(int manaRegenAmount);

    public virtual void AddHealPower(int amount)
    {

    }
}
