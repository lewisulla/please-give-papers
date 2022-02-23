using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private Rigidbody2D _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private Vector3 _dragOffset;
    private void OnMouseDown()
    {
        _dragOffset = transform.position - GetMousePos();
        _rb.bodyType = RigidbodyType2D.Kinematic;

    }

    private void OnMouseUp()
    {
        if (GetComponent<ObjectSwapper>().GetMode()==0)
        {
            
            _rb.bodyType = RigidbodyType2D.Dynamic;
            
        }
        else
        {
            
            _rb.bodyType = RigidbodyType2D.Static;
        }
        
    }

    void OnMouseDrag()
    {
        transform.position = GetMousePos() + _dragOffset;
    }

    Vector3 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
