using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PinSelector : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Image _pinImage;
    [SerializeField]
    private TextMeshProUGUI _nameDisplay;
    [SerializeField]
    private TextMeshProUGUI _descriptionDisplay;
    [Space(5)]
    [Header("Events")]
    [SerializeField]
    private UnityEvent<ActionPinData> _onClick;

    private ActionPinData _currentPin;

    public void ApplyData(ActionPinData data)
    {
        _currentPin = data;

        _pinImage.sprite = data.uiSprite;
        _nameDisplay.text = data.name;
        _descriptionDisplay.text = data.description;
    }

    public void OnClick()
    {
        Debug.Log("Selector clicked");
        _onClick?.Invoke(_currentPin);
    }

    public void AddOnClickListener(UnityAction<ActionPinData> listener)
    {
        _onClick.AddListener(listener);
    }

    public void RemoveOnClickListener(UnityAction<ActionPinData> listener)
    {
        _onClick.RemoveListener(listener);
    }
}
