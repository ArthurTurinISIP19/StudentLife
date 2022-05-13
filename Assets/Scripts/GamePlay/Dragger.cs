using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : AbstractProgress
{
    void OnMouseDrag()
    {
        transform.position = GetMousePos(); //Vector3.MoveTowards(transform.position, GetMousePos() + _dragOffset, _speed * Time.deltaTime);
    }

    Vector3 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
