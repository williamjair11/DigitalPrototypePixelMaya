using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _velocitySpeed = 5;
    private float z;
    private float x;
    private float y;

    InputController _inputcontroller= null;

    private void Awake()
    {
        _inputcontroller = GetComponent<InputController>();
    }
    void Update()
    {
        Move();
    }

    void Move() 
    {
        Vector2  Input = _inputcontroller.MoveInput();
      
        transform.position += transform.forward * Input.y  *_velocitySpeed  *Time.deltaTime;
        transform.position += transform.right * Input.x * _velocitySpeed * Time.deltaTime;
    }
}
