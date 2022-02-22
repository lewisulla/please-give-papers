using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItem",menuName = "Game Data/Create Item")]
public class ItemData : ScriptableObject
{
    public ItemType type;
    public GameObject itemPrefab;

}

public enum ItemType
{
    Passport,
    ID
}