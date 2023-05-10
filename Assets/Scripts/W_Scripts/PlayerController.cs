using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(InputController))]
public class PlayerController : MonoBehaviour
{
    
    [Header("Player states")]

    [SerializeField]
    private Rigidbody _rbPlayer;

    [SerializeField]
    private float _velocitySpeed = 5f;

    [SerializeField]
    private float _jumpForce = 10f;

    [SerializeField]
    private float _jumpEnergyCost;

    private Vector3 _lastPlayerPosition;

    [SerializeField] private bool _canMove = true;

   


    [Header("Ball Energy")]

    public Transform _positionBall;

    [SerializeField]
    private GameObject _EnergyBall;

    [SerializeField]
    private float _ballEnergyCost;

    [SerializeField]
    private float _timeNextShot;

    [SerializeField]
    private float _ballForceForward;

    [SerializeField]
    private float _ballForceUp;

    private bool _shotAvailable = true;

    [Header("Flash")]

    [SerializeField] private float _timeNextFlash;

    [SerializeField] private float _costFlashEnergy;

    private bool _flashIsAvaible = true;

    [SerializeField] private float _timeStunFlash;

    private bool _flashButtonActivated = false;

    [Header("Events")]

    [SerializeField]
    private UnityEvent<float> _jumpEvent;

    [SerializeField]
    private UnityEvent<float> _energyBallEvent;

    [SerializeField]
    private UnityEvent<float> _enemyStunEvent;

    [SerializeField]
    private UnityEvent<float> _decrementEnergyFlashEvent;


    [Header("Inicialition objects")]
    InputController _inputcontroller = null;
    IsGrounded _isGrounded;
    public Light playerGameObject;

    private void Awake()
    {
        _inputcontroller = GetComponent<InputController>();
        _isGrounded = GetComponent<IsGrounded>();
        playerGameObject = GetComponent<Light>();
    }
    void Update()
    {
        if (_canMove) 
        {
            Move();
            savePosition();
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
            //_jumpEvent.Invoke(_jumpEnergyCost);
        }
    }

    public void throwButton() 
    {
        if (_shotAvailable)
        {
            GameObject _temporaryEnergyBall = Instantiate(_EnergyBall, _positionBall.transform.position, _positionBall.transform.rotation);
            Rigidbody _rb = _temporaryEnergyBall.GetComponent<Rigidbody>();
            _rb.AddForce(_temporaryEnergyBall.transform.forward * _ballForceForward, ForceMode.Impulse);
            _rb.AddForce(_temporaryEnergyBall.transform.up * _ballForceUp, ForceMode.Impulse);
            _energyBallEvent.Invoke(_ballEnergyCost); 
            _shotAvailable = false;
            StartCoroutine(waitForNextShot());
            Destroy(_temporaryEnergyBall, 5f);
        }
        else 
        {
            //Play sound ball in cooldown
        }
    }

    public void flashButton() 
    {
            _flashButtonActivated = true;
    }

    IEnumerator waitForNextShot()
    {
        yield return new WaitForSeconds(_timeNextShot);
        _shotAvailable = true;
    }

    IEnumerator waitForNextFlash()
    {
        yield return new WaitForSeconds(_timeNextFlash);
        _flashIsAvaible = true;
        playerGameObject.intensity = 0;
        playerGameObject.range = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (_flashButtonActivated && _flashIsAvaible) 
        {
            if(other.tag == "Enemy") 
            {
                _decrementEnergyFlashEvent.Invoke(_costFlashEnergy);
                _enemyStunEvent.Invoke(_timeStunFlash);
                _flashIsAvaible = false;
                _flashButtonActivated = false;
                playerGameObject.intensity = 100;
                playerGameObject.range = 30;
                StartCoroutine(waitForNextFlash());
            }
        }
    }

    public IEnumerator StunTime(float value) 
    {
        _canMove = false;
        yield return new WaitForSeconds(value);
        _canMove = true;
    }
}
