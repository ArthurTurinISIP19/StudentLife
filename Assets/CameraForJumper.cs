using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForJumper : MonoBehaviour
{
    [SerializeField] private GameObject _targetGameObject;

    void Update()
    {
        transform.position =  new Vector3(_targetGameObject.transform.position.x, transform.position.y, transform.position.z);    
    }
}
