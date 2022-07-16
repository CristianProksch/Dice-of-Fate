using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PinSelectionDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject _container;
    [SerializeField]
    private PinSelector[] _selectors;
    [SerializeField]
    private PlayerBehavior _player;

    [Header("Settings")]
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
        int amount = _selectors.Length;
        var collections = _player._actionPinCollectionCollection.GetRandomActionPinCollections(amount);

        for (int i = 0; i < amount; i++)
        {
            if (i < collections.Count)
            {
                _selectors[i].ApplyData(collections[i]);
            }
            else
            {
                _selectors[i].Hide();
            }
        }
    }

    private void OnPinCollectionSelected(ActionPinCollection data)
    {
        _onPinCollectionSelected?.Invoke(data);
        _player._actionPinCollectionCollection.SetActionPinCollectionPlaced(data);
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
