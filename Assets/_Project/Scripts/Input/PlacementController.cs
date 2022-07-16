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

    private ActionPinCollection _pinToPlace;

    private void Start()
    {
        InputController.AddMouseUpListener(() => TryPlacePin());
        _selectionDisplay.AddPinSelectedListener(SetPinToPlace);
    }

    private void OnDestroy()
    {
        InputController.RemoveMouseUpListener(() => TryPlacePin());
        _selectionDisplay.RemovePinSelectedListener(SetPinToPlace);
    }

    private void TryPlacePin(bool advancePhase = true)
    {
        if (TurnController.GetCurrentPhase() != TurnPhase.Placement)
        {
            return;
        }

        if (_pinToPlace == null)
        {
            return;
        }

        _grid.GetGridPosition(InputController.GetMousePosition(), out int x, out int y);

        if (!_grid.IsValidGridPosition(x, y))
        {
            return;
        }

        //_grid.PlacePin(InputController.GetMousePosition(), _pinToPlace.pins);
        _pinToPlace = null;

        if (advancePhase)
        {
            TurnController.NextPhase();
        }
    }

    private void SetPinToPlace(ActionPinCollection pinData)
    {
        _pinToPlace = pinData;
    }
}
