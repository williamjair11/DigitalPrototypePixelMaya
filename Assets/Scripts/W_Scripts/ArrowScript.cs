using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidBody;
    [SerializeField] Transform _tip;
    [SerializeField] LayerMask _layerMask;
    private bool _isStoped = true;
    private Vector3 _lastPosition = Vector3.zero;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isStoped)
        return;

        //Rotate
        _rigidBody.MoveRotation(Quaternion.LookRotation(_rigidBody.velocity, transform.up));

        
        //Collision
        if(Physics.Linecast(_lastPosition, _tip.position, out RaycastHit hitInfo, _layerMask))
        {
            Stop();
        }

        //Store position
        _lastPosition = _tip.transform.position;
    }

    void Stop()
    {
        _isStoped = true;
         _rigidBody.isKinematic = true;
         _rigidBody.useGravity = false;
    }

    public void Fire(float _arrowForce)
    {
        transform.position = Camera.main.transform.position;
        transform.position += transform.forward * 2;
        transform.rotation = Camera.main.transform.rotation;
        _isStoped = false;
        transform.parent = null;
        _rigidBody.isKinematic = false;
        _rigidBody.useGravity = true;
        _rigidBody.AddForce(Camera.main.transform.forward * _arrowForce);
        Destroy(gameObject, 5f);
    }
}
