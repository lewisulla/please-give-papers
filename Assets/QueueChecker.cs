using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueChecker : MonoBehaviour
{
    private MiniCharController controller;
    private void Start()
    {
        controller = transform.parent.GetComponent<MiniCharController>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        string objectTag = col.gameObject.tag;
        
        if (objectTag == "MinimapChar")
        {
            controller._rb.velocity = Vector2.zero;
            controller._anim.SetTrigger("Stop"); 
        }

    }

    private void OnCollisionExit2D(Collision2D col)
    {
        string objectTag = col.gameObject.tag;

        if (objectTag == "MinimapChar")
        {
            controller._rb.velocity = new Vector2(2, 0);
            controller._anim.SetTrigger("Continue");
        }
    }
}