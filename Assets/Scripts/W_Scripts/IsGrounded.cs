using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    [SerializeField] private GameObject _groundedObject;
    [SerializeField] private float _distanceGroundRaycast;
    [SerializeField] public bool _floorDetected;
    [SerializeField] private float _distanceFromGround;

    void Awake()
    {
        _groundedObject = gameObject;
    }
    void Update()
    {
        ObjectIsGrounded();
        DistanceFromGround();
    }

    public float DistanceFromGround() 
    {
        RaycastHit _raycastHit;
        Ray _rayDistance = new Ray(_groundedObject.transform.position, Vector3.down);
        if (Physics.Raycast(_rayDistance, out _raycastHit)) 
        {
            _distanceFromGround = _raycastHit.distance;
                    
        }
        return _distanceFromGround;
    }

    void ObjectIsGrounded() 
    {
        RaycastHit _raycastHit;
        Ray _ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(_ray, out _raycastHit, _distanceGroundRaycast))
        {            
            _floorDetected = true;
        }
        else 
        {
            _floorDetected = false;
        }
    }
}
