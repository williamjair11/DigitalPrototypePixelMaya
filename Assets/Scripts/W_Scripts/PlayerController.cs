using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _velocitySpeed = 5;
    private Vector3 _lastPlayerPosition;
    InputController _inputcontroller= null;

    private void Awake()
    {
        _inputcontroller = GetComponent<InputController>();
    }
    void Update()
    {
        Move();
        savePosition();
    }

    void Move() 
    {
        Vector2  Input = _inputcontroller.MoveInput();
      
        transform.position += transform.forward * Input.y  *_velocitySpeed  *Time.deltaTime;
        transform.position += transform.right * Input.x * _velocitySpeed * Time.deltaTime;
    }
    public Vector3 savePosition() 
    {
        if(_lastPlayerPosition != null && _lastPlayerPosition != transform.position) 
        {
            _lastPlayerPosition = transform.position;
            Debug.Log(_lastPlayerPosition);
        }

        return _lastPlayerPosition;
    }


}
