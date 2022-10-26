using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParellaxBackground : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private float parelaxEffect;
    private Vector3 lastCameraPosition;

    private float _startPoint;
    private float _length;

    private void Start()
    {
        _startPoint = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
        if (cameraPosition == null)
        {
            cameraPosition = Camera.main.transform;
        }
    }

    private void LateUpdate()
    {
        var camPos = cameraPosition.position;
        var deltaMovement = _startPoint + (camPos.x * parelaxEffect);
        camPos.x = deltaMovement;
        transform.position = camPos;

        var distanceCheck = camPos.x * (1 - parelaxEffect);
        if (distanceCheck > _startPoint + _length)
        {
            _startPoint += _length;
        }
        else if (distanceCheck < _startPoint - _length)
        {
            _startPoint -= _length;
        }

    }
}
