using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskButton : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        StartCoroutine(ColorTint());
    }

    private IEnumerator ColorTint()
    {
        _spriteRenderer.color = Color.white;
        yield return new WaitForSecondsRealtime(0.25f);
        _spriteRenderer.color = new Color32(175, 175, 175, 255);
    } 
}
