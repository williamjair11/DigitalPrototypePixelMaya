using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InputController))]
public class PlayerController : MonoBehaviour
{
    private Vector3 _lastPlayerPosition;
    private bool isJump ;
    private bool _holdingBall=true;
    private Rigidbody _rbBall;

    public Transform _camera;

    [SerializeField]
    private GameObject _EnergyBall;

    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private float _velocitySpeed = 5f;

    [SerializeField]
    private float _jumpForce = 10f;

    [SerializeField]
    private float _jumpEnergyCost;

    [SerializeField]
    private float _ballEnergyCost;

    [SerializeField]
    private float _ballForceForward;

    [SerializeField]
    private float _ballForceUp;


    InputController _inputcontroller = null;
    IsGrounded _isGrounded;

    [SerializeField]
    private UnityEvent<float> _jumpEvent;

    [SerializeField]
    private UnityEvent<float> _energyBallEvent;

    private void Awake()
    {
        _inputcontroller = GetComponent<InputController>();
        _isGrounded = GetComponent<IsGrounded>();        
        _rbBall = _EnergyBall.GetComponent<Rigidbody>();
        //_rbBall.useGravity = false;       
    }
    void Update()
    {
        Move();
        savePosition();
        Jump();
        throwball();
    }

    private void LateUpdate()
    {
        if (_holdingBall)
        {
            _EnergyBall.transform.position = _camera.transform.position + _camera.forward * 2;
        }
        throwball();
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
            //Debug.Log(_lastPlayerPosition);
        }

        return _lastPlayerPosition;
    }

    public void Jump() 
    {
        isJump = Input.GetKeyDown(KeyCode.G);
        bool isGamePadJump = _inputcontroller.Jump();       

        if (_isGrounded._floorDetected && isJump)
        {
            _rb.AddForce(new Vector3(0, _jumpForce, 0), ForceMode.Impulse);
            _jumpEvent.Invoke(_jumpEnergyCost);
        }
    }

    public void JumpButton() 
    {
        if (_isGrounded._floorDetected)
        {
            _rb.AddForce(new Vector3(0, _jumpForce, 0), ForceMode.Impulse);
            _jumpEvent.Invoke(_jumpEnergyCost);
        }
    }

    public void throwButton() 
    {
        if (_holdingBall)
        {
            if (Input.GetMouseButton(1))
            {
                _rbBall.AddForce(_camera.forward * _ballForceForward);
                _rbBall.AddForce(_camera.up * _ballForceUp);
                _rb.useGravity = true;
                _holdingBall = false;
                _energyBallEvent.Invoke(_ballEnergyCost);
            }
        }
    }
    
    public void throwball() 
    {
        
        if (_holdingBall) 
        {                   
            if (Input.GetMouseButton(1))              
            {                           
                _rbBall.AddForce(_camera.forward * _ballForceForward);
                _rbBall.AddForce(_camera.up * _ballForceUp);
                _rb.useGravity = true;
                _holdingBall = false;
                _energyBallEvent.Invoke(_ballEnergyCost);
            }
        }
    }
}
