using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellDisplay : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer cellRenderer;

    public void Show()
    {
        cellRenderer.enabled = true;
    }

    public void Hide()
    {
        cellRenderer.enabled = false;
    }

    public void SetColor(Color color)
    {
        cellRenderer.color = color;
    }
}
