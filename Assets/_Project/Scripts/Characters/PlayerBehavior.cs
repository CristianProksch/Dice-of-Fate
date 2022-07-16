using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    [SerializeField]
    private int _health;

    [SerializeField]
    private int _maxHealth;

    [SerializeField]
    private int _shield;

    [SerializeField]
    public ActionPinCollectionCollection _actionPinCollectionCollection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {

    }

    public void TakeDamage(int ammount)
    {
        _health -= ammount;
        if (_health <= 0)
            Die();
    }

    public void Heal(int ammount)
    {
        _health = Mathf.Min(_maxHealth, _health + ammount);
    }
}
