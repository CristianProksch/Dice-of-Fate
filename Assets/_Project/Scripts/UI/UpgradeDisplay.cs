using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDisplay : PinSelectionDisplay
{
    internal override void Start()
    {
        GameController.AddStartUpgradingListener(() => Show(true));
        foreach (var selector in _selectors)
        {
            selector.AddOnClickListener(OnPinCollectionSelected);
        }

        Hide();
    }

    internal override void OnDestroy()
    {
        GameController.RemoveStartUpgradingListener(() => Show(true));
        foreach (var selector in _selectors)
        {
            selector.RemoveOnClickListener(OnPinCollectionSelected);
        }
    }

    public override void Show(bool randomizeSelection = false)
    {
        if (GameController.GetCurrentPhase() != GamePhase.Upgrading)
        {
            return;
        }

        _container.SetActive(true);

        if (randomizeSelection)
        {
            RandomizePinSelection();
        }
    }

    public override void RandomizePinSelection()
    {
        int amount = _selectors.Length;
        var collections = ActionPinCollectionManager.Instance.getCollectionsForPlayerLevel(GameController.GetCurrentLevel());

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

        if (collections.Count == 0)
        {
            OnPinCollectionSelected(null);
            return;
        }
    }

    internal override void OnPinCollectionSelected(ActionPinCollection data)
    {
        _onPinCollectionSelected?.Invoke(data);
        _player._actionPinCollectionCollection.Add(data);
        GameController.NextPhase();
        Hide();
    }
}
