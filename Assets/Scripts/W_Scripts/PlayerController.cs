using DG.Tweening;
using System;
using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.XInput;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerController : MonoBehaviour
{
    public enum ControlType { Normal, Vr }

    [Header("Player")]

    [NonSerialized]public ControlType _controlType;

    [SerializeField] private Rigidbody _rbPlayer;

    [SerializeField] public float _initialSpeedPlayer = 5f;

    [SerializeField] private float _runSpeed;

    private float _velocitySpeed;

    private bool _movePlayer;

    private bool _isRunning;

    private Vector3 _initialPosition;

    [SerializeField] private bool _inGreenZone = false;

    [SerializeField] private float _slowSpeed;

    [SerializeField] private float _jumpForce = 10f;

    private Vector3 _lastPlayerPosition;

    [SerializeField] private bool _canMove = true;

    [Header("Events")]

    [SerializeField] private UnityEvent<float> _jumpEvent;

    [SerializeField] private UnityEvent<float, string> _fallEvent;


    [Header("Inicialition objects")]

    InputController _inputController = null;

    IsGrounded _isGrounded;

    EnergyController _energyController;

    Camera _camera;



    private void Awake()
    {
        _inputController = FindObjectOfType<InputController>();
        _isGrounded = GetComponent<IsGrounded>();
        _energyController = FindObjectOfType<EnergyController>();
        _velocitySpeed = _initialSpeedPlayer;
        _initialPosition = transform.position;
        _controlType = ControlType.Normal;
        _camera = Camera.main;

    }
    void Update()
    {

        switch (_controlType) 
        {
            case ControlType.Normal:
                Move();
                break;

            case ControlType.Vr:
                MoveVr();
                break;
        }

        OffStage();
     
        savePosition();

        RechargeEnergyMove();

        JumpIsPressed();

        RunIsPressed();       
    }

    void Move()
    {
        Vector2 Input = _inputController.MoveInput();

        transform.position += transform.forward * Input.y * _velocitySpeed * Time.deltaTime;
        transform.position += transform.right * Input.x * _velocitySpeed * Time.deltaTime;

        if (Input.x > 0 || Input.y > 0) { _movePlayer = true; }
        else { _movePlayer = false; }
    }

    void MoveVr()
    {
        Vector2 Input = _inputController.MoveInput();

        Vector3 velocity = _camera.transform.forward * Input.y * _velocitySpeed;
        transform.position += velocity * Time.deltaTime;

        if (Input.y > 0) { _movePlayer = true; }
        else { _movePlayer = false; }
    }
    public Vector3 savePosition()
    {
        if (_lastPlayerPosition != null && _lastPlayerPosition != transform.position)
        {
            _lastPlayerPosition = transform.position;
        }

        return _lastPlayerPosition;
    }

    public void Jump()
    {
        if (_isGrounded._floorDetected)
        {
            _rbPlayer.AddForce(new Vector3(0, _jumpForce, 0), ForceMode.Impulse);
        }
    }

    public void Run()
    {
        if (_isGrounded._floorDetected && _movePlayer)
        {
            _velocitySpeed = _runSpeed;
            _energyController.ReduceRunEnergy();
            _isRunning = true;
        }
    }

    public void ResetPosition() 
    {
        transform.position = _initialPosition;
    }

    void RechargeEnergyMove() 
    {
        if (_energyController._regeneratingEnergy == true)
        {
            _velocitySpeed = _slowSpeed;
        }
        else
        {
            _velocitySpeed = _initialSpeedPlayer;
        }
    }

    void JumpIsPressed() 
    {
        if (_inputController.Jump())
        {
            Jump();
        }
    }

    void RunIsPressed() 
    {

    }

    void OffStage() 
    {
        if (transform.position.y < -20)
        {
            ResetPosition();
            _fallEvent.Invoke(20f, "Fall");
        }
    }
}
