using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newTextData",menuName = "Game Data/Create Text Data")]
public class TextData : ScriptableObject
{
    public List<string> data;
}