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
    private GameObject _EnergyBallPrefab;

    [SerializeField]
    private GameObject _GreenEnergyBallPrefab;

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

    [SerializeField] private bool _flashIsAvaible = true;

    [SerializeField] private float _timeStunFlash;

    [SerializeField] private bool _rangeToFlash = false;

    [SerializeField] private Light _lightPlayer;

    [Header("Inicialition objects")]

    EnergyController _energyController;

    [SerializeField] private PlayerController _playerGameObject;

    private InputController _inputController;

    private TweenManager _tweenManager;

    private PickupController _pickupController;

   
    private void Awake()
    {
        _energyController = FindObjectOfType<EnergyController>();
        _playerGameObject = GetComponent<PlayerController>();
        _inputController = FindObjectOfType<InputController>();
        _tweenManager = FindObjectOfType<TweenManager>();
        _pickupController = FindObjectOfType<PickupController>();
        DOTween.Init();
    }
    private void Update()
    {
            if (_inputController.ThrowBallEnergy()) { throwBall(); }

            if (_inputController.FlashHability()) { CastFlashHability(); }                     
    }

    public void throwBall()
    {
        GameObject _temporaryEnergyBall = null;
        Rigidbody _rb = null;

        if (_shotAvailable && _energyController._regeneratingEnergy == false && _pickupController.heldObj == null && _energyController.ConsultCurrentEnergy() >= _ballEnergyCost)
        {
            switch (_energyController._energyType)
            {
                case EnergyController.EnergyTypes.White:
                    _temporaryEnergyBall = Instantiate(_EnergyBallPrefab, _positionBall.transform.position, _positionBall.transform.rotation);
                    _rb = _temporaryEnergyBall.GetComponent<Rigidbody>();
                    break;
                case EnergyController.EnergyTypes.Green:
                    _temporaryEnergyBall = Instantiate(_GreenEnergyBallPrefab, _positionBall.transform.position, _positionBall.transform.rotation);
                    _rb = _temporaryEnergyBall.GetComponent<Rigidbody>();
                    break;
            }
            
            _rb.AddForce(_temporaryEnergyBall.transform.forward * _ballForceForward, ForceMode.Impulse);
            _rb.AddForce(_temporaryEnergyBall.transform.up * _ballForceUp, ForceMode.Impulse);
            _energyBallEvent.Invoke(_ballEnergyCost);
            StartCoroutine(waitForNextShot());
            Destroy(_temporaryEnergyBall, 10f);
        }
        else
        {
            
        }
    }

    public void CastFlashHability() 
    {
        if (_flashIsAvaible && _rangeToFlash && _energyController._regeneratingEnergy == false) 
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
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") 
        { 
            _rangeToFlash = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy") 
        { 
            _rangeToFlash = false; 
        }
    }

    IEnumerator waitForNextShot()
    {
        _shotAvailable = false;
        yield return new WaitForSeconds(_timeNextShot);
        _shotAvailable = true;
    }

    IEnumerator waitForNextFlash()
    {
        _flashIsAvaible = false;
        yield return new WaitForSeconds(_timeNextFlash);
        _flashIsAvaible = true;
    }
}
