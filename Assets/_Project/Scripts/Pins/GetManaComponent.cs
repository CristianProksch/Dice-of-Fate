using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetManaComponent : PinComponentBase
{
    public int manaRegenAmount;

    internal override void TriggerAction(IPinOwner owner)
    {
        owner.AddMana(manaRegenAmount);
    }
}
