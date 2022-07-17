using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPin : MonoBehaviour
{

    public PinComponentBase[] pinComponents;
    public Rigidbody2D rb2d;

    private IPinOwner _owner;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.isKinematic = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check if collision was with the right object

        Execute();
    }

    void Execute()
    {
        if(AudioManager.instance != null) {
            AudioManager.instance.Play("HitPin");
        }
        foreach (PinComponentBase comp in pinComponents)
        {
            comp.TriggerAction(_owner);
        }
    }

    public void SetOwner(IPinOwner newOwner)
    {
        _owner = newOwner;
    }
}
