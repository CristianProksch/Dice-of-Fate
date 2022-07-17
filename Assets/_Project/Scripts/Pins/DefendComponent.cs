using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendComponent : PinComponentBase
{
    public int defensePower;

    internal override void TriggerAction(IPinOwner owner)
    {
        owner.AddArmourPower(defensePower);
    }
}
