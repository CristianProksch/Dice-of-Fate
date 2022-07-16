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
    private UnityEvent<ActionPinCollection> _onPinCollectionSelected;

    private void Start()
    {
        TurnController.AddStartPlacementListener(() => Show(true));
        foreach (var selector in _selectors)
        {
            selector.AddOnClickListener(OnPinCollectionSelected);
        }
    }

    private void OnDestroy()
    {
        TurnController.RemoveStartPlacementListener(() => Show(true));
        foreach (var selector in _selectors)
        {
            selector.RemoveOnClickListener(OnPinCollectionSelected);
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

    private void OnPinCollectionSelected(ActionPinCollection data)
    {
        _onPinCollectionSelected?.Invoke(data);
        Hide();
    }

    public void AddPinCollectionSelectedListener(UnityAction<ActionPinCollection> listener)
    {
        _onPinCollectionSelected.AddListener(listener);
    }

    public void RemovePinCollectionSelectedListener(UnityAction<ActionPinCollection> listener)
    {
        _onPinCollectionSelected.RemoveListener(listener);
    }
}
