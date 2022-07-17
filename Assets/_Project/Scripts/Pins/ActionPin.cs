using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPin : MonoBehaviour
{
    public string displayName;
    public PinComponentBase[] pinComponents;
    public Rigidbody2D rb2d;
    public Animator pinAnimator;

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
            int random = Random.Range(1, 5);
            AudioManager.instance.Play("HitPin" + random.ToString());
        }
        if(pinAnimator != null) {
            pinAnimator.SetTrigger("Wobble");
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
