using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Die : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onDieFinished;

    public void AddDieFinishedListener(UnityAction listener)
    {
        _onDieFinished.AddListener(listener);
    }

    public void RemoveDieListener(UnityAction listener)
    {
        _onDieFinished.RemoveListener(listener);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Destruct")
        {
            _onDieFinished?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
