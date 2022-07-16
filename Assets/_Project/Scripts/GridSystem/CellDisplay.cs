using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CellDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        _onMouseEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _onMouseExit?.Invoke();
    }
}
