using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Pin Data", fileName = "New Pin")]
public class ActionPinCollection : ScriptableObject
{
    public Sprite uiSprite;
    public List<ActionPin> pins;
    public string displayName;
    public string description;
}
