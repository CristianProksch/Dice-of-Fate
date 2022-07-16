using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Pin Data", fileName = "New Pin")]
public class ActionPinCollection : ScriptableObject
{
    [SerializeField]
    private Sprite _uiSprite;
    public Sprite UiSprite { get { return _uiSprite; } }

    [SerializeField]
    private List<ActionPin> _pins;
    public List<ActionPin> Pins { get { return _pins; } }

    [SerializeField]
    private string _displayName;
    public string DisplayName { get { return _displayName; } }

    [SerializeField]
    private string _description;
    public string Description { get { return _description; } }
}
