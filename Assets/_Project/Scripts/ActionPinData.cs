using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Pin Data", fileName = "New Pin")]
public class ActionPinData : ScriptableObject
{
    public Sprite uiSprite;
    public ActionPin prefab;
    public string displayName;
    public string description;
}
