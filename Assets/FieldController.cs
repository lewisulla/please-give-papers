using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    [SerializeField] private int mode;

    private void OnTriggerEnter2D(Collider2D col)
    {
        var circleCollider2D = col as CircleCollider2D;
        if (circleCollider2D != null)
        {
            circleCollider2D.GetComponent<ObjectSwapper>().ObjectSwap(mode);
        }
    }

}
