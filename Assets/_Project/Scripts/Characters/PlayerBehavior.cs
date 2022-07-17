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
    private Animator _slashAnimator;
    [SerializeField]
    public ActionPinCollectionCollection _actionPinCollectionCollection;
    [SerializeField]
    private Sprite _sprite;

    public Sprite PlayerSprite { get { return _sprite; } }

    public int MaxHealth { get { return _maxHealth; } }
    private int _currentHealth;
    public int MaxMana { get { return _maxMana; } }
    private int _currentMana;
    public int CurrentMana { get { return _currentMana; } }
    public int CurrentHealth { get { return _currentHealth; } }
    private int _currentArmour;

    private int _attackPower;
    public int AttackPower { get { return _attackPower; } }
    private int _armourPower;
    public int ArmourPower { get { return _armourPower; } }
    private int _healPower;
    public int HealPower { get { return _healPower; } }

    private void Start()
    {
        InitializeHealth();
        InitializeMana();
        TurnController.AddStartMonsterPlacementListener(() => { ResetArmour(); ResetPinPowers(); });
    }

    #region Damageable
    public void InitializeHealth()
    {
        _currentHealth = _maxHealth;
    }

    public void InitializeMana()
    {
        _currentMana = 0;
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
        _slashAnimator.SetTrigger("Play");
        AudioManager.instance.Play("EnemyAttack");
    }

    public void HealDamage(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        AudioManager.instance.Play("Heal");
    }

    public void AddMana(int amount)
    {
        _currentMana += amount;
        _currentMana = Mathf.Clamp(_currentMana, 0, _maxMana);
        AudioManager.instance.Play("Heal");
    }

    public void AddArmour(int amount)
    {
        _currentArmour += amount;
        AudioManager.instance.Play("Defense");
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

    public int GetCurrentMana()
    {
        return _currentMana;
    }

    public void loseMana(int amount)
    {
        _currentMana -= amount;
    }
    #endregion
}
