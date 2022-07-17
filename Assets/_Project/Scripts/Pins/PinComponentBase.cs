using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinComponentBase : MonoBehaviour
{
    internal virtual void TriggerAction(IPinOwner owner)
    {
        throw new NotImplementedException();
    }
}
