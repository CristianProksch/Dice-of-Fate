using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour, IDamageable, IPinOwner
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private int _maxMana;
    [SerializeField]
    public ActionPinCollectionCollection _actionPinCollectionCollection;
    [SerializeField]
    private Sprite _sprite;

    public Sprite PlayerSprite { get { return _sprite; } }

    public int MaxHealth { get { return _maxHealth; } }
    private int _currentHealth;
    public int CurrentHealth { get { return _currentHealth; } }
    private int _currentArmour;

    private int _attackPower;
    public int AttackPower { get { return _attackPower; } }
    private int _armourPower;
    public int ArmourPower { get { return _armourPower; } }
    private int _healPower;
    public int HealPower { get { return _healPower; } }

    public int MaxMana { get { return _maxHealth; } }
    private int _currentMana;

    private void Start()
    {
        InitializeHealth();
        TurnController.AddStartMonsterPlacementListener(() => { ResetArmour(); ResetPinPowers(); });
    }

    #region Damageable
    public void InitializeHealth()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int amount)
    {
        var actualDamage = amount - _currentArmour;
        _currentArmour -= amount;
        if (_currentArmour < 0)
        {
            _currentArmour = 0;
        }

        if (actualDamage <= 0)
        {
            return;
        }

        _currentHealth -= actualDamage;
    }

    public void HealDamage(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }

    public void AddArmour(int amount)
    {
        _currentArmour += amount;
    }

    public void ResetArmour()
    {
        _currentArmour = 0;
    }

    public void CheckDeath()
    {
        if (_currentHealth <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        GameController.OnPlayerHasDied();
    }
    #endregion

    #region Pin Owner
    public void AddAttackPower(int amount)
    {
        _attackPower += amount;
    }

    public void AddArmourPower(int amount)
    {
        _armourPower += amount;
    }

    public void AddHealPower(int amount)
    {
        _healPower += amount;
    }

    public void ResetPinPowers()
    {
        _attackPower = 0;
        _armourPower = 0;
        _healPower = 0;
    }

    public void AddMana(int amount)
    {
        _currentMana += amount;
        _currentMana = Mathf.Clamp(_currentMana, 0, _maxMana);
    }
    #endregion
}
