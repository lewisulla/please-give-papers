using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChar",menuName = "Game Data/Create Character")]
public class CharacterData : ScriptableObject
{
    public string charName;
    public int age;
    public string destination;
    public Sprite portrait;

    public List<ItemData> items;

}
