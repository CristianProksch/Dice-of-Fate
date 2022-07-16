using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinComponentBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal virtual void TriggerAction()
    {
        throw new NotImplementedException();
    }
}
