using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    [SerializeField] private GameObject _objectTransform;
    private float heigth = 100f;
    
    [NonSerialized]public bool _floorDetected;
    private float _distanceFromGround;
    private RaycastHit _raycastHit;
    void Update()
    {
        ObjectIsGrounded();
        DistanceFromGround();
    }

    public float DistanceFromGround() 
    {
        Ray _rayDistance = new Ray(_objectTransform.transform.position, Vector3.down);
        if (Physics.Raycast(_rayDistance, out _raycastHit)) 
        {          
            if(_raycastHit.collider.tag == "ground") 
            {
                _distanceFromGround = _raycastHit.distance;
            }         
        }
        return _distanceFromGround;
    }

    void ObjectIsGrounded() 
    {
        Ray _rayGround = new Ray(_objectTransform.transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down , Color.red);
        if (Physics.Raycast(_rayGround, 0.4f))
        {
            _floorDetected = true;
        }
        else 
        {
            _floorDetected = false;
        }
    }
}
