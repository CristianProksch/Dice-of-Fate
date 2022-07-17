using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PinSelectionDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    internal GameObject _container;
    [SerializeField]
    internal PinSelector[] _selectors;
    [SerializeField]
    internal PlayerBehavior _player;

    [Header("Settings")]
    [SerializeField]
    internal UnityEvent<ActionPinCollection> _onPinCollectionSelected;

    internal virtual void Start()
    {
        TurnController.AddStartPlacementListener(() => Show(true));
        foreach (var selector in _selectors)
        {
            selector.AddOnClickListener(OnPinCollectionSelected);
        }

        Hide();
    }

    internal virtual void OnDestroy()
    {
        TurnController.RemoveStartPlacementListener(() => Show(true));
        foreach (var selector in _selectors)
        {
            selector.RemoveOnClickListener(OnPinCollectionSelected);
        }
    }

    public virtual void Show(bool randomizeSelection = false)
    {
        if (GameController.GetCurrentPhase() != GamePhase.Combat)
        {
            return;
        }

        _container.SetActive(true);

        if (randomizeSelection)
        {
            RandomizePinSelection();
        }
    }

    public void Hide()
    {
        _container.SetActive(false);
    }

    public virtual void RandomizePinSelection()
    {
        int amount = _selectors.Length;
        var collections = _player._actionPinCollectionCollection.GetRandomActionPinCollections(amount);

        for (int i = 0; i < amount; i++)
        {
            if (i < collections.Count)
            {
                _selectors[i].ApplyData(collections[i]);
                _selectors[i].Show();
            }
            else
            {
                _selectors[i].Hide();
            }
        }

        if (collections.Count == 0)
        {
            OnPinCollectionSelected(null);
            return;
        }
    }

    internal virtual void OnPinCollectionSelected(ActionPinCollection data)
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
