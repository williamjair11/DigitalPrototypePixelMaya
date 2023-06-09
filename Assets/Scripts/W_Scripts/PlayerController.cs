using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(InputController))]
public class PlayerController : MonoBehaviour
{
    
    [Header("Player")]

    [SerializeField] private Rigidbody _rbPlayer;

    [SerializeField] public float _initialSpeedPlayer = 5f;

    [SerializeField] private float _runSpeed;

    private float _velocitySpeed;

    private bool _movePlayer;

    private bool _isRunning;

    [SerializeField] private float _slowSpeed;

    [SerializeField] private float _jumpForce = 10f;

    private Vector3 _lastPlayerPosition;

    [Header("Events")]

    [SerializeField] private UnityEvent<float> _jumpEvent;


    [Header("Inicialition objects")]

    InputController _inputcontroller = null;

    IsGrounded _isGrounded;

    EnergyController _energyController;

    TweenManager _tweenManager;

    private void Awake()
    {
        _inputcontroller = GetComponent<InputController>();
        _isGrounded = GetComponent<IsGrounded>();
        _energyController = GetComponent<EnergyController>();
        _tweenManager = FindObjectOfType<TweenManager>();
        _velocitySpeed = _initialSpeedPlayer;
        
    }
    void Update()
    {
        EnergyController.EnergysTypes _powerStatePlayer = _energyController._typeEnergy;

        switch (_powerStatePlayer) 
        {
            case EnergyController.EnergysTypes.Normal:
                PowerStateNormal();
                break;
            case EnergyController.EnergysTypes.Green:
                PowerStateGreen();
                break;
        }

            Move();
            savePosition();

        if (_energyController._regeneratingEnergy == true)
        {
            _velocitySpeed= _slowSpeed;
        }
        else 
        {
            _velocitySpeed = _initialSpeedPlayer;
        }

        if (_inputcontroller.Jump()) 
        {
            Jump();
        }

        if (_inputcontroller.RunPlayer()) 
        {
            if(_energyController._regeneratingEnergy == false) { Run(); }           
        }
        else 
        {
            if(_energyController._regeneratingEnergy == true) { _velocitySpeed = _slowSpeed; }
            else 
            { 
                _velocitySpeed = _initialSpeedPlayer;
                _isRunning = false;
            }
        }
    }

    void Move() 
    {
        Vector2  Input = _inputcontroller.MoveInput();
        
        transform.position += transform.forward * Input.y  *_velocitySpeed  *Time.deltaTime;
        transform.position += transform.right * Input.x * _velocitySpeed * Time.deltaTime;

        if(Input.x > 0 || Input.y > 0) { _movePlayer = true; }
        else { _movePlayer = false; }
    }
    public Vector3 savePosition() 
    {
        if(_lastPlayerPosition != null && _lastPlayerPosition != transform.position) 
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

    public void PowerStateNormal() 
    {
        if (!_isRunning) { _velocitySpeed = _initialSpeedPlayer; }       
    }

    public void PowerStateGreen()
    {
        _tweenManager.TweenPowerGreenEnergyOn();
    }
}
