using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Pin Data", fileName = "New Pin")]
public class ActionPinCollection : ScriptableObject
{
    [SerializeField]
    private int _id;
    public int id { get => _id; }
    [SerializeField]
    private int _level;
    public int level { get => _level; }
    [SerializeField]
    private Sprite _uiSprite;
    public Sprite UiSprite { get => _uiSprite; }

    [SerializeField]
    private List<ActionPin> _pins;
    public List<ActionPin> Pins { get => _pins; }

    [SerializeField]
    private string _displayName;
    public string DisplayName { get => _displayName; }

    [SerializeField]
    private string _description;
    public string Description { get => _description; }
}
