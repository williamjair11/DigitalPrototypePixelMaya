using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(InputController))]
public class PlayerController : MonoBehaviour
{
    
    [Header("Player")]

    [SerializeField]
    private Rigidbody _rbPlayer;

    [SerializeField]
    public float _velocitySpeed = 5f;

    [SerializeField]
    private float _jumpForce = 10f;

    [SerializeField]
    private float _jumpEnergyCost;

    private Vector3 _lastPlayerPosition;

    [SerializeField] private bool _canMove = true;

    [SerializeField] private float _timeToRecoveryCurrentSpeed;

    [Header("Events")]

    [SerializeField]
    private UnityEvent<float> _jumpEvent;


    [Header("Inicialition objects")]
    InputController _inputcontroller = null;
    IsGrounded _isGrounded;
    EnergyController _energyController;

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
        
        if(_energyController._currentEnergy <= 0) 
        {
            StartCoroutine(SlowMove());
        }

        if (_inputcontroller.Jump()) 
        {
            JumpButton();
        }
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

    IEnumerator SlowMove() 
    {
        _velocitySpeed = _velocitySpeed / 2;
        yield return new WaitForSeconds(_timeToRecoveryCurrentSpeed);
        _velocitySpeed = _velocitySpeed * 2;
    }
}
