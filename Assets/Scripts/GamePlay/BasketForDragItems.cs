using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketForDragItems : AbstractProgress
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "DragItem")
        {
            collision.gameObject.SetActive(false);
            ProgressUp();
        }
    }
}
