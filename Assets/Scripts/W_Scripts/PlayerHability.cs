using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.XInput;

public class PlayerHability : MonoBehaviour
{
    private bool _reloadEnergy = false;

    [Header("Events")]

    [SerializeField]
    private UnityEvent<float> _energyBallEvent;

    [SerializeField]
    private UnityEvent<float> _enemyStunEvent;

    [SerializeField]
    private UnityEvent<float> _decrementEnergyFlashEvent;

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

    private bool _rangeToFlash = false;

    [Header("Inicialition objects")]

    EnergyController _energyController;

    [SerializeField] private Light playerlight;

    [SerializeField] private PlayerController _playerGameObject;

    private InputController _inputController;

   
    private void Awake()
    {
        playerlight = GetComponent<Light>();
        _energyController = GetComponent<EnergyController>();
        _playerGameObject = GetComponent<PlayerController>();
        _inputController = GetComponent<InputController>();
    }
    private void Update()
    {
        if (_inputController.ThrowBallEnergy()) { throwBall(); }

        if(_inputController.FlashHability()) { flash(); }
    }

    public void throwBall()
    {
        if (_shotAvailable && _reloadEnergy == false && _energyController.ConsultCurrentEnergy() >= _ballEnergyCost)
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

    public void flash()
    {
        if (_rangeToFlash)
        {
            _flashButtonActivated = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            _rangeToFlash = true;
            if (_flashButtonActivated && _flashIsAvaible && _rangeToFlash && _energyController.ConsultCurrentEnergy() >= _costFlashEnergy)
            {
                _decrementEnergyFlashEvent.Invoke(_costFlashEnergy);
                _enemyStunEvent.Invoke(_timeStunFlash);
                _flashIsAvaible = false;
                _flashButtonActivated = false;
                playerlight.intensity = 100;
                playerlight.range = 30;
                StartCoroutine(waitLight());
            }           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy") { _rangeToFlash = false; }
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
    }

    IEnumerator waitLight() 
    {
        playerlight.intensity = 100;
        playerlight.range = 10;
        yield return new WaitForSeconds(1);
        playerlight.intensity = 0;
        playerlight.range = 0;

        StartCoroutine(waitForNextFlash());
    }

}
