using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPinOwner
{
    public virtual void AddAttackPower(int amount)
    {

    }

    public virtual int GetCurrentMana()
    {
        return 0;
    }

    public virtual void loseMana(int amount)
    {

    }

    public virtual void AddArmourPower(int amount)
    {

    }

    public virtual void AddHealPower(int amount)
    {

    }

    public virtual void AddMana(int amount)
    {

    }
}
