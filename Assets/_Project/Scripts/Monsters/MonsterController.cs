using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct MonsterPin
{
    public MonsterPinType type;
    public ActionPin prefab;
}

public class MonsterController : MonoBehaviour, IDamageable, IPinOwner
{
    [SerializeField]
    private PinGrid _grid;
    [SerializeField]
    private ActionPin _neutralPinPrefab;
    [SerializeField]
    private List<MonsterPin> _monsterPinPrefabList;

    private Dictionary<MonsterPinType, ActionPin> _monsterPinPrefabs;

    [HideInInspector]
    public MonsterData _currentMonster;
    private Dictionary<ActionPin, MonsterPinType> _monsterPins;
    private List<ActionPin> _replacedPins;

    private int _maxMonsterHealth;
    public int MaxMonsterHealth { get { return _maxMonsterHealth; } }
    private int _currentMonsterHealth;
    public int CurrentMonsterHealth { get { return _currentMonsterHealth; } }
    private int _currentMonsterArmour;
    public int CurrentMonsterArmour { get { return _currentMonsterArmour; } }

    private int _attackPower;
    public int AttackPower { get { return _attackPower; } }
    private int _armourPower;
    public int ArmourPower { get { return _armourPower; } }
    private int _healPower;
    public int HealPower { get { return _healPower; } }

    public Sprite MonsterSprite { get { return _currentMonster.monsterSprite; } }

    private void Start()
    {
        _monsterPinPrefabs = new Dictionary<MonsterPinType, ActionPin>();
        foreach (var prefab in _monsterPinPrefabList)
        {
            _monsterPinPrefabs[prefab.type] = prefab.prefab;
        }

        _monsterPins = new Dictionary<ActionPin, MonsterPinType>();
        _replacedPins = new List<ActionPin>();

        TurnController.AddStartMonsterPlacementListener(() => { ReplaceNeutralPins(); ResetArmour(); ResetPinPowers(); });
    }

    public void SpawnMonster(MonsterData monster)
    {
        Clear();
        _currentMonster = monster;

        foreach(var pinData in monster.pins)
        {
            var pin = _grid.PlacePin(pinData.xPosition, pinData.yPosition, _neutralPinPrefab);
            _monsterPins.Add(pin, pinData.type);
        }
        InitializeHealth();
    }

    private void Clear()
    {
        foreach(var pin in _monsterPins.Keys)
        {
            _grid.RemovePin(pin);
        }

        _monsterPins.Clear();
        _replacedPins.Clear();
    }

    private void ReplaceNeutralPins()
    {
        var neutralPins = _monsterPins.Keys.Except(_replacedPins).ToList();

        for (int i = 0; i < _currentMonster.pinsPerTurn; i++)
        {
            if (neutralPins.Count() <= 0)
            {
                break;
            }

            var pin = neutralPins.ElementAt(Random.Range(0, neutralPins.Count()));
            var type = _monsterPins[pin];
            neutralPins.Remove(pin);
            _monsterPins.Remove(pin);
            _grid.RemovePin(pin);

            var replacement = _grid.PlacePin(pin.transform.position, _monsterPinPrefabs[type]);
            _monsterPins.Add(replacement, type);
            replacement.SetOwner(this);
        }

        TurnController.NextPhase();
    }

    #region Damageable
    public void InitializeHealth()
    {
        _maxMonsterHealth = _currentMonster.maxHealth;
        _currentMonsterHealth = _currentMonster.maxHealth;
    }

    public void TakeDamage(int amount)
    {
        var actualDamage = amount - _currentMonsterArmour;
        _currentMonsterArmour -= amount;
        if (_currentMonsterArmour < 0)
        {
            _currentMonsterArmour = 0;
        }

        if (actualDamage <= 0)
        {
            return;
        }

        _currentMonsterHealth -= actualDamage;
    }

    public void HealDamage(int amount)
    {
        _currentMonsterHealth += amount;
        _currentMonsterHealth = Mathf.Clamp(_currentMonsterHealth, 0, _maxMonsterHealth);
    }

    public void AddArmour(int amount)
    {
        _currentMonsterArmour += amount;
    }

    public void ResetArmour()
    {
        _currentMonsterArmour = 0;
    }

    public void CheckDeath()
    {
        if (_currentMonsterHealth <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        GameController.OnMonsterHasDied();
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

    public void AddMana(int manaRegenAmount)
    {
    }
    #endregion
}
