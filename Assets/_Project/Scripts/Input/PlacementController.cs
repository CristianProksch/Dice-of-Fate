using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController : MonoBehaviour
{
    #region Inspector
    [SerializeField]
    private PinGrid _grid;
    [SerializeField]
    private PinSelectionDisplay _selectionDisplay;
    #endregion

    private Queue<ActionPin> _pinsToPlace;

    private void Start()
    {
        InputController.AddMouseUpListener(() => TryPlacePin());
        _selectionDisplay.AddPinCollectionSelectedListener(SetPinsToPlace);
    }

    private void OnDestroy()
    {
        InputController.RemoveMouseUpListener(() => TryPlacePin());
        _selectionDisplay.RemovePinCollectionSelectedListener(SetPinsToPlace);
    }

    private void TryPlacePin(bool advancePhase = true)
    {
        if (TurnController.GetCurrentPhase() != TurnPhase.Placement)
        {
            return;
        }

        if (InputController.IsMouseOverUI())
        {
            return;
        }

        if (_pinsToPlace == null || _pinsToPlace.Count <= 0)
        {
            return;
        }

        _grid.GetGridPosition(InputController.GetMousePosition(), out int x, out int y);

        if (!_grid.IsValidGridPosition(x, y, true))
        {
            return;
        }

        var pin = _pinsToPlace.Dequeue();
        _grid.PlacePin(InputController.GetMousePosition(), pin);

        if (_pinsToPlace.Count <= 0 && advancePhase)
        {
            TurnController.NextPhase();
        }
    }

    private void SetPinsToPlace(ActionPinCollection pinData)
    {
        _pinsToPlace = new Queue<ActionPin>(pinData.pins);
    }
}
