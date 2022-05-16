using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : AbstractProgress
{
    private Vector2 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    void OnMouseDrag()
    {
        transform.position = GetMousePos(); //Vector3.MoveTowards(transform.position, GetMousePos() + _dragOffset, _speed * Time.deltaTime);
        
        if(CameraSwitcherMain.GameActivated == false)
        {
            BackToStartLocation();
        }
    }

    private void OnMouseUp()
    {
        if(gameObject.activeSelf == true)
        {
            BackToStartLocation();
        }
    }

    private void BackToStartLocation()
    {
        transform.position = _startPosition;
    }

    Vector3 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
