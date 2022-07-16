using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PinSelectionDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject _container;
    [SerializeField]
    private PinSelector[] _selectors;

    [Header("Settings")]
    [SerializeField]
    private ActionPinCollection[] _availablePins;
    [SerializeField]
    private UnityEvent<ActionPinCollection> _onPinSelected;

    private void Start()
    {
        TurnController.AddStartPlacementListener(() => Show(true));
        foreach (var selector in _selectors)
        {
            selector.AddOnClickListener(OnPinSelected);
        }
    }

    private void OnDestroy()
    {
        TurnController.RemoveStartPlacementListener(() => Show(true));
        foreach (var selector in _selectors)
        {
            selector.RemoveOnClickListener(OnPinSelected);
        }
    }

    public void Show(bool randomizeSelection = false)
    {
        if (randomizeSelection)
        {
            RandomizePinSelection();
        }

        _container.SetActive(true);
    }

    public void Hide()
    {
        _container.SetActive(false);
    }

    public void RandomizePinSelection()
    {
        foreach(var selector in _selectors)
        {
            selector.ApplyData(_availablePins[Random.Range(0, _availablePins.Length)]);
        }
    }

    private void OnPinSelected(ActionPinCollection data)
    {
        _onPinSelected?.Invoke(data);
        Hide();
    }

    public void AddPinSelectedListener(UnityAction<ActionPinCollection> listener)
    {
        _onPinSelected.AddListener(listener);
    }

    public void RemovePinSelectedListener(UnityAction<ActionPinCollection> listener)
    {
        _onPinSelected.RemoveListener(listener);
    }
}
