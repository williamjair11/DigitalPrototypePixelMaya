using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InputController))]
public class PlayerController : MonoBehaviour
{
    

    private Vector3 _lastPlayerPosition;
    private bool isJump ;

    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private float _velocitySpeed = 5f;
    [SerializeField]
    private float _jumpForce = 10f;

    [SerializeField]
    private float _jumpEnergy;

    InputController _inputcontroller = null;
    IsGrounded _isGrounded;
    EnergyController _energyController = null;

    [SerializeField]
    private UnityEvent<float> _jumpEvent;

    private void Awake()
    {
        _inputcontroller = GetComponent<InputController>();
        _isGrounded = GetComponent<IsGrounded>();
        _energyController = GetComponent<EnergyController>();
    }
    void Update()
    {
        Move();
        savePosition();
        Jump();
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
            _jumpEvent.Invoke(_jumpEnergy);
        }
    }

    public void JumpButton() 
    {
        if (_isGrounded._floorDetected)
        {
            _rb.AddForce(new Vector3(0, _jumpForce, 0), ForceMode.Impulse);
            _jumpEvent.Invoke(_jumpEnergy);
        }
    }
}
