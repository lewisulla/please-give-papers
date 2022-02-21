using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwapper : MonoBehaviour
{
    private int _mode = 0;


    public void ObjectSwap(int mode)
    {
        if (_mode == mode)
        {
            return;
        }

        transform.GetChild(_mode).gameObject.SetActive(false);
        _mode = mode;
        var activeChild = transform.GetChild(_mode);
        BoxCollider2D holderCol = transform.GetComponent<BoxCollider2D>();
        BoxCollider2D childCol = activeChild.GetComponent<BoxCollider2D>();
        holderCol.offset = childCol.offset;
        holderCol.size = childCol.size*childCol.GetComponent<Transform>().localScale;
        activeChild.gameObject.SetActive(true);

        
    }

    public int GetMode()
    {
        return _mode;
    }
}
