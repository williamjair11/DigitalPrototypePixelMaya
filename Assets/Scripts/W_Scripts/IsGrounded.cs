using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectTransform;
    [SerializeField]
    private float heigth = 100f;
    
    public bool _floorDetected;
    public float _distanceFromGround;
    private RaycastHit _raycastHit;
    void Update()
    {
        ObjectIsGrounded();
        DistanceFromGround();
    }

    public float DistanceFromGround() 
    {
        Ray _rayDistance = new Ray(_objectTransform.transform.position, Vector3.down);
        //Debug.DrawRay(transform.position, Vector3.down * heigth, Color.red);
        if (Physics.Raycast(_rayDistance, out _raycastHit)) 
        {          
            if(_raycastHit.collider.tag == "ground") 
            {
                _distanceFromGround = _raycastHit.distance;
                //Debug.Log("La distancia respecto al suelo es: "+ _distanceFromGround);
            }         
        }
        return _distanceFromGround;
    }

    void ObjectIsGrounded() 
    {
        Ray _rayGround = new Ray(_objectTransform.transform.position, Vector3.down);
        if (Physics.Raycast(_rayGround, 1f))
        {
            _floorDetected = true;
            //Debug.Log("Objeto en el suelo");
        }
        else 
        {
            _floorDetected = false;
            //Debug.Log("Objeto en el aire");
        }
    }
}
