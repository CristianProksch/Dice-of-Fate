using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CellDisplay : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _cellRenderer;
    [SerializeField]
    private UnityEvent _onMouseEnter;
    [SerializeField]
    private UnityEvent _onMouseExit;

    public void Show()
    {
        _cellRenderer.enabled = true;
    }

    public void Hide()
    {
        _cellRenderer.enabled = false;
    }

    public void SetColor(Color color)
    {
        _cellRenderer.color = color;
    }

    public void AddMouseEnterListener(UnityAction listener)
    {
        _onMouseEnter.AddListener(listener);
    }

    public void AddMouseExitListener(UnityAction listener)
    {
        _onMouseExit.AddListener(listener);
    }

    private void OnMouseEnter()
    {
        _onMouseEnter?.Invoke();
    }

    private void OnMouseExit()
    {
        _onMouseExit?.Invoke();
    }
}
