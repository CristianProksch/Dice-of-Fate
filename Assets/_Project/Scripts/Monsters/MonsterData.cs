using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterPinType
{
    None,
    Attack,
    Heal,
    Defend
}

[System.Serializable]
public class MonsterPinData
{
    public int xPosition;
    public int yPosition;
    public MonsterPinType type;
}

[CreateAssetMenu(menuName = "Custom/Monster", fileName = "Monster")]
public class MonsterData : ScriptableObject
{
    public string displayName;
    public Sprite monsterSprite;
    public int maxHealth;
    public int pinsPerTurn;
    public List<MonsterPinData> pins;
}
