using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System;

public class WhiteEnergy : MonoBehaviour
{

    [SerializeField] public float _initialEnergy;

    [NonSerialized] public float _currentValueEnergy;

    [SerializeField] public float _SpeedRecoveryEnergyPassive;

    [SerializeField] public float _speedAbsorbEnergy;

    [SerializeField] public float _timeAfkToRecoveryEnergy;

    [SerializeField] public float _timeToRechargeAllEnergy;

    [SerializeField] public float _costRunEnergy;

    [Header("Energy Bar references")]
    [SerializeField] private Slider _energySlider;
    [SerializeField] private GameObject _energyContainer;

    [Header("Twween manager References")]
    private TweenManager _tweenManager;

    private EnergyController _energyController;

    private PlayerController _playerController;

    private bool _counterActivate = false;
    private float _currentTime;
    public bool _regeneratingEnergy = false;

    [Header("Events Normal Energy")]
    [SerializeField] private UnityEvent _onEnergyFullEvent;
    [SerializeField] private UnityEvent _onEnergyChangedEvent;
    [SerializeField] private UnityEvent _onEnergyEndsEvent;
    [SerializeField] private UnityEvent _regeneratingEnergyEvent;

    void Start()
    {
        _currentValueEnergy = _initialEnergy;
        _energySlider.maxValue = _initialEnergy;
        _currentTime = _timeAfkToRecoveryEnergy;
        _tweenManager = FindObjectOfType<TweenManager>();
        _energyController = FindObjectOfType<EnergyController>();
        _playerController = FindObjectOfType<PlayerController>();
        DOTween.Init();
    }

    void Update()
    {
        VerifyEnergyState();

        if(_energyController._energyType == EnergyController.EnergyTypes.White) { _energyContainer.SetActive(true); }
        else { _energyContainer.SetActive(false); }
    }

    #region Normal Energy
    public void RegenerateEnergy(float value) //Method to increment a certain value of energy
    {
        float num = _currentValueEnergy + value;
        if (num <= _initialEnergy)
        {
            _currentValueEnergy += value;
            _energySlider.DOValue(_currentValueEnergy, 1);
            _onEnergyChangedEvent.Invoke();
            _regeneratingEnergy = false;
            RestarCounterEnergy();
        }
        else
        {
            num = 0;
            Debug.Log("No se puede sobrepasar el nivel de energía");
        }
    }

    public void ReduceEnergy(float value) //Method to reduce a certain value of energy
    {
        float num = _currentValueEnergy - value;
        if (num >= 0)
        {
            _currentValueEnergy -= value;
            _energySlider.DOValue(_currentValueEnergy, 1);
            _onEnergyChangedEvent.Invoke();
            RestarCounterEnergy();
            DesactivateCounterEnergy();
        }
        else
        {
            num = 0;
            Debug.Log("No se puede reducir más la energia");
        }
    }

    public float ConsultCurrentEnergy() // Method to obtained the curren Energy 
    {
        return _currentValueEnergy;
    }
    IEnumerator RegeneratingAllEnergy()  // Method to regenerate all the energy
    {
        _playerController.SlowSpeed();
        _energySlider.value = 0f;
        _energySlider.DOValue(_initialEnergy, _timeToRechargeAllEnergy);
        _tweenManager.TweenRegenerateAllEnergy();
        _regeneratingEnergy = true;
        yield return new WaitForSeconds(_timeToRechargeAllEnergy);
        _regeneratingEnergy = false;
        _playerController.ReturnsToNormalSpeed();
        RestarCounterEnergy();
    }

    public void ReduceRunEnergy() // METHOD TO REDUCE ENERGY WHILE THE PLAYER RUNS
    {
        if (_currentValueEnergy != 0 && _regeneratingEnergy == false)
        {
            _currentValueEnergy -= _costRunEnergy * Time.deltaTime;
            _energySlider.value = _currentValueEnergy;
            RestarCounterEnergy();
            DesactivateCounterEnergy();
        }
    }

    public void IncrementEnergy()  //Method to increase energy over time
    {
        _currentValueEnergy += _speedAbsorbEnergy * Time.deltaTime;
        _energySlider.value = _currentValueEnergy;
        RestarCounterEnergy();
    }

    public void RegeneratingPasiveEnergy() // Method for the player to enter a state of recovering energy
                                           // (It is different from regenerating all energy since it is dependent on a counter)
    {
        if (_currentValueEnergy <= _initialEnergy)
        {
            _currentValueEnergy += _SpeedRecoveryEnergyPassive * Time.deltaTime;
            _energySlider.value = _currentValueEnergy;
        }
        else
        {
            RestarCounterEnergy();
            DesactivateCounterEnergy();
        }
    }

    public void RestarCounterEnergy() //Method to restar the counter time
    {
        _currentTime = _timeAfkToRecoveryEnergy;
    }

    public void ActivateCounterEnergy() //Activate the counter
    {
        _counterActivate = true;
    }

    public void DesactivateCounterEnergy()  //Desactivate the counter
    {
        _counterActivate = false;
    }

    public void VerifyEnergyState()
    {
        if (_regeneratingEnergy == false)
        {
            _currentTime -= Time.deltaTime;
        }

        if (_currentTime <= 0)
        {
            _counterActivate = true;
        }

        if (_counterActivate)
        {
            RegeneratingPasiveEnergy();
        }

        if (_currentValueEnergy <= 0)
        {
            _onEnergyEndsEvent.Invoke();
            _regeneratingEnergy = true;
            _currentValueEnergy = _initialEnergy;
            StartCoroutine(RegeneratingAllEnergy());
        }
    }
    #endregion
}
