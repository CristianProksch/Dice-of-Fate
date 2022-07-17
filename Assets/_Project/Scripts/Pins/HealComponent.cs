using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealComponent : PinComponentBase
{
    public int healAmount;

    internal override void TriggerAction(IPinOwner owner)
    {
        owner.AddHealPower(healAmount);
    }
}
