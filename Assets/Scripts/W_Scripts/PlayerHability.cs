using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class PlayerHability : MonoBehaviour
{
    [Header("Events")]

    [SerializeField]
    private UnityEvent<float> _energyBallEvent;

    [SerializeField]
    private UnityEvent<float> _greenEnergyBallEvent;

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

    [Header("Green Ball Energy")]

    [SerializeField] private float _greenBallEnergyCost;

    private bool _shootGreenEnergyIsAvaible = true;

    [SerializeField] private float _timeNextGreenEnergyBall;

    [Header("Flash")]

    [SerializeField] private float _timeNextFlash;

    [SerializeField] private float _costFlashEnergy;

    private bool _flashIsAvaible = true;

    [SerializeField] private float _timeStunFlash;

    private bool _rangeToFlash = false;

    [SerializeField] private Light _lightPlayer;

    [Header("Inicialition objects")]

    EnergyController _energyController;

    [SerializeField] private PlayerController _playerGameObject;

    private InputController _inputController;

    private TweenManager _tweenManager;

   
    private void Awake()
    {
        _energyController = GetComponent<EnergyController>();
        _playerGameObject = GetComponent<PlayerController>();
        _inputController = GetComponent<InputController>();
        _tweenManager= FindObjectOfType<TweenManager>();
        DOTween.Init();
    }
    private void Update()
    {
        EnergyController.EnergysTypes _currentTypeEnergy = _energyController._typeEnergy;

        if (_currentTypeEnergy == EnergyController.EnergysTypes.Normal) 
        {
            if (_inputController.ThrowBallEnergy()) { throwBall(); }

            if (_inputController.FlashHability()) { CastFlashHability(); }          
        }
        
        if( _currentTypeEnergy == EnergyController.EnergysTypes.Green)
        {
            if (_inputController.ThrowBallEnergy()) { ThrowGreenBallEnergy(); }

            if (_inputController.FlashHability()) { CastFlashHability(); }
        }       
    }

    public void throwBall()
    {
        if (_shotAvailable && _energyController.ConsultCurrentEnergy() >= _ballEnergyCost && _energyController._regeneratingEnergy == false)
        {
            GameObject _temporaryEnergyBall = Instantiate(_EnergyBall, _positionBall.transform.position, _positionBall.transform.rotation);
            Rigidbody _rb = _temporaryEnergyBall.GetComponent<Rigidbody>();
            _rb.AddForce(_temporaryEnergyBall.transform.forward * _ballForceForward, ForceMode.Impulse);
            _rb.AddForce(_temporaryEnergyBall.transform.up * _ballForceUp, ForceMode.Impulse);
            _energyBallEvent.Invoke(_ballEnergyCost);
            StartCoroutine(waitForNextShot());
            Destroy(_temporaryEnergyBall, 5f);
        }
        else
        {
            //Play sound ball in cooldown
            if(_energyController.ConsultCurrentEnergy()<= _ballEnergyCost) { _tweenManager.TweenInsufficientEnergySlider(); }
        }
    }

    public void CastFlashHability() 
    {
        if (_rangeToFlash && _flashIsAvaible && _energyController.ConsultCurrentEnergy() >= _costFlashEnergy && _energyController._regeneratingEnergy == false) 
        {
            _decrementEnergyFlashEvent.Invoke(_costFlashEnergy);
            _enemyStunEvent.Invoke(_timeStunFlash);

            Sequence _flashSequence = DOTween.Sequence();
            _flashSequence.Insert(0, _lightPlayer.DOIntensity(100f, 0.5f));
            _flashSequence.Insert(2, _lightPlayer.DOIntensity(0, 0.3f));
            _tweenManager.TweenFlashHabilitySlider();
            StartCoroutine(waitForNextFlash());
        }
        else
        {
            //Play sound FLASH in cooldown
            if (_energyController.ConsultCurrentEnergy() <=  _costFlashEnergy) { _tweenManager.TweenInsufficientEnergySlider(); }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy") { _rangeToFlash = true; }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy") { _rangeToFlash = false; }
    }

    IEnumerator waitForNextShot()
    {
        _shotAvailable = false;
        yield return new WaitForSeconds(_timeNextShot);
        _shotAvailable = true;
    }

    IEnumerator waitForNextGreenEnergyBall()
    {
        _shootGreenEnergyIsAvaible= false;
        yield return new WaitForSeconds(_timeNextGreenEnergyBall);
        _shootGreenEnergyIsAvaible = true;
    }

    IEnumerator waitForNextFlash()
    {
        _flashIsAvaible = false;
        yield return new WaitForSeconds(_timeNextFlash);
        _flashIsAvaible = true;
    }

    public void ThrowGreenBallEnergy() 
    {
        if(_shootGreenEnergyIsAvaible) 
        {
            Debug.Log("Lanzando bola verde");
            StartCoroutine(waitForNextGreenEnergyBall());
            _greenEnergyBallEvent.Invoke(_greenBallEnergyCost);
        }       
    }
}
