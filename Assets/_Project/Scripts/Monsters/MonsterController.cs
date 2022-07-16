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

public class MonsterController : MonoBehaviour
{
    [SerializeField]
    private PinGrid _grid;
    [SerializeField]
    private ActionPin _neutralPinPrefab;
    [SerializeField]
    private List<MonsterPin> _monsterPinPrefabList;

    private Dictionary<MonsterPinType, ActionPin> _monsterPinPrefabs;

    private MonsterData _currentMonster;
    private Dictionary<ActionPin, MonsterPinType> _monsterPins;
    private List<ActionPin> _replacedPins;

    private void Start()
    {
        _monsterPinPrefabs = new Dictionary<MonsterPinType, ActionPin>();
        foreach (var prefab in _monsterPinPrefabList)
        {
            _monsterPinPrefabs[prefab.type] = prefab.prefab;
        }

        _monsterPins = new Dictionary<ActionPin, MonsterPinType>();
        _replacedPins = new List<ActionPin>();

        TurnController.AddStartMonsterPlacementListener(() => ReplaceNeutralPins());
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
    }

    private void Clear()
    {
        foreach(var pin in _monsterPins.Keys)
        {
            Destroy(pin.gameObject);
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

            var replacement = _grid.PlacePin(pin.transform.position, _monsterPinPrefabs[type]);
            _monsterPins.Add(replacement, type);
        }

        TurnController.NextPhase();
    }
}
