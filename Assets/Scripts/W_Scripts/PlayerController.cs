using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (_isGrounded._floorDetected && isJump)
        {
            _rb.AddForce(new Vector3(0, _jumpForce, 0), ForceMode.Impulse);
        }
    }

    public void JumpButton() 
    {
        if (_isGrounded._floorDetected)
        {
            _rb.AddForce(new Vector3(0, _jumpForce, 0), ForceMode.Impulse);
        }
    }


}
