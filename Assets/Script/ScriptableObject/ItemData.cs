using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable,
    TimeLimit,
    OneUse
}



public enum ConsumableType
{
    Health,
    Dash
}
[Serializable]
public class ItemDataConsumbale
{
    public ConsumableType type;
    public float value;
}
[CreateAssetMenu(fileName = "Item" , menuName = "NewItem")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumbale[] consumables;
}
