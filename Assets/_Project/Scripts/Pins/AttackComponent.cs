using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : PinComponentBase
{
    public int attackPower;

    internal override void TriggerAction(IPinOwner owner)
    {
        owner.AddAttackPower(attackPower);
    }
}
