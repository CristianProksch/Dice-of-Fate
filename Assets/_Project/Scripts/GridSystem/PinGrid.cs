using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinGrid : MonoBehaviour
{
    #region Inspector
    [Header("Grid Settings")]
    [SerializeField]
    private int _width;
    [SerializeField]
    private int _height;
    [SerializeField]
    private float _cellSize;
    [SerializeField]
    private CellDisplay _cellDisplayPrefab;

    [Space(5)]
    [Header("Debug")]
    [SerializeField]
    private Transform _debugObject;
    #endregion

    private CellDisplay[,] _gridBackground;
    private ActionPin[,] _grid;

    private void Start()
    {
        _gridBackground = new CellDisplay[_width, _height];
        _grid = new ActionPin[_width, _height];

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _gridBackground[x, y] = Instantiate(_cellDisplayPrefab, GetWorldPosition(x, y, true), Quaternion.identity, transform);
            }
        }
    }

    private Vector3 GetWorldPosition(int x, int y, bool getCenter = false)
    {
        var result = transform.position + (new Vector3(x, y) * _cellSize);

        if (getCenter)
        {
            result += new Vector3(1f, 1f, 0f) * _cellSize * 0.5f;
        }

        return result;
    }

    private void GetGridPosition(Vector3 worldPosition, out int x, out int y)
    {
        var temp = worldPosition - transform.position;
        x = Mathf.FloorToInt(temp.x / _cellSize);
        y = Mathf.FloorToInt(temp.y / _cellSize);
    }

    public void SetPin(int x, int y, ActionPin value)
    {
        _grid[x, y] = value;
    }

    public void SetPin(Vector3 worldPosition, ActionPin value)
    {
        GetGridPosition(worldPosition, out int x, out int y);
        SetPin(x, y, value);
    }

    public ActionPin GetPin(int x, int y)
    {
        return _grid[x, y];
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Gizmos.DrawWireCube(GetWorldPosition(x, y, true), Vector3.one * _cellSize);
            }
        }
    }
}
