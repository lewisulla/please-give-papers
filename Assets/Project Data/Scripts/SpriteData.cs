using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSpriteData",menuName = "Game Data/Create Sprite Data")]
public class SpriteData : ScriptableObject
{
    public List<Sprite> sprites;
}