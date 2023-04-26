using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(InputController))]
public class PlayerController : MonoBehaviour
{
    private Vector3 _lastPlayerPosition;

    [SerializeField]
    private float _velocitySpeed = 5f;

    [SerializeField]
    private float _jumpForce = 10f;

    [SerializeField]
    private float _jumpEnergyCost;

    [SerializeField]
    private UnityEvent<float> _jumpEvent;

    [SerializeField]
    private Rigidbody _rbPlayer;

    InputController _inputcontroller = null;
    IsGrounded _isGrounded;

    private void Awake()
    {
        _inputcontroller = GetComponent<InputController>();
        _isGrounded = GetComponent<IsGrounded>();               
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
        }

        return _lastPlayerPosition;
    }

    public void JumpButton() 
    {
        if (_isGrounded._floorDetected)
        {
            _rbPlayer.AddForce(new Vector3(0, _jumpForce, 0), ForceMode.Impulse);
        }
    }
}
