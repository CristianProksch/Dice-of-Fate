using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PinPreview
{
    public MonsterPinType type;
    public GameObject prefab;
}

public class MonsterEditor : PinGrid
{
    [SerializeField]
    private MonsterData _monsterToEdit;
    [SerializeField]
    private Transform _previewParent;
    [SerializeField]
    private List<PinPreview> _previewList;

    private MonsterPinData[,] _monsterPins;
    private Dictionary<MonsterPinType, GameObject> _previews;

    internal override void Start()
    {
        base.Start();

        InputController.AddMouseUpListener(() => AdjustMonsterPins());

        _previews = new Dictionary<MonsterPinType, GameObject>();
        foreach(var preview in _previewList)
        {
            _previews[preview.type] = preview.prefab;
        }

        _monsterPins = new MonsterPinData[_width, _height];
        CopyFromScriptableObject();
        DrawPreviews();
    }

    private void AdjustMonsterPins()
    {
        GetGridPosition(InputController.GetMousePosition(), out int x, out int y);

        if (!IsValidGridPosition(x, y))
        {
            return;
        }

        if (_monsterPins[x, y] == null)
        {
            _monsterPins[x, y] = new MonsterPinData()
            {
                xPosition = x,
                yPosition = y,
                type = MonsterPinType.Attack
            };
        }
        else
        {
            int pinType = (int)_monsterPins[x, y].type;
            pinType++;
            pinType %= Enum.GetValues(typeof(MonsterPinType)).Length;

            _monsterPins[x, y].type = (MonsterPinType)pinType;
        }

        WriteToScriptableObject();
        DrawPreviews();
    }

    private void WriteToScriptableObject()
    {
        List<MonsterPinData> pins = new List<MonsterPinData>();

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (_monsterPins[x, y] != null && _monsterPins[x, y].type != MonsterPinType.None)
                {
                    pins.Add(_monsterPins[x, y]);
                }
            }
        }

        _monsterToEdit.pins = pins;
    }

    private void CopyFromScriptableObject()
    {
        foreach(var pin in _monsterToEdit.pins)
        {
            if (IsValidGridPosition(pin.xPosition, pin.yPosition))
            {
                _monsterPins[pin.xPosition, pin.yPosition] = pin;
            }
        }
    }

    private void DrawPreviews()
    {
        foreach(Transform child in _previewParent)
        {
            Destroy(child.gameObject);
        }

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (_monsterPins[x, y] != null && _monsterPins[x, y].type != MonsterPinType.None)
                {
                    Instantiate(_previews[_monsterPins[x, y].type], GetWorldPosition(_monsterPins[x, y].xPosition, _monsterPins[x, y].yPosition, true), Quaternion.identity, _previewParent);
                }
            }
        }
    }
}
