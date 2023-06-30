using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System;

public class EnergyController : MonoBehaviour
{
    public enum EnergysTypes {Normal, Green}
    public EnergysTypes _typeEnergy;

    [Header("Variables normal Energy")]
    [SerializeField] public float _initialEnergy;
    [NonSerialized] public float _currentEnergy;
    [SerializeField] private float _SpeedRecoveryEnergyPassive;
    [SerializeField] private float _speedAbsorbEnergy;
    [SerializeField] private float _timeAfkToRecoveryEnergy;
    [SerializeField] public float _timeToRechargeAllEnergy;
    [SerializeField] private float _costRunEnergy;
    private bool _counterActivate = false;  
    private float _currentTime;
    public bool _regeneratingEnergy = false;
    [NonSerialized] public bool _normalEnergyIsActivated = true;

    [Header("Variables Green Energy")]
    [SerializeField] public float _initialValueGreenEnergy;
    [NonSerialized] public float _currentGreenEnergy;
    [SerializeField] private float _speedAbsorbGreenEnergy;
    [SerializeField] private float _speedDecrementGreenEnergy;
    [NonSerialized] public bool _greenEnergyIsActivated;
    
    [Header("Events Normal Energy")]
    [SerializeField] private UnityEvent _onEnergyFullEvent;
    [SerializeField] private UnityEvent _onEnergyChangedEvent;
    [SerializeField] private UnityEvent _onEnergyEndsEvent;
    [SerializeField] private UnityEvent _regeneratingEnergyEvent;
    [SerializeField] private UnityEvent _desactivatedSliderEnergy;
    [SerializeField] private UnityEvent _activatedSliderEnergy;

    [Header("Events Green Energy")]
    [SerializeField] private UnityEvent _desactivatedSliderGreenEnergy;
    [SerializeField] private UnityEvent _activatedSliderGreenEnergy;

    [Header("Energy Bar references")]
    [SerializeField] Slider _energySlider;
    [SerializeField] Slider _greenEnergySlider;

    [Header("Twween manager References")]
    private TweenManager _tweenManager;
    void Start()
    {
        _currentEnergy = _initialEnergy;
        _energySlider.maxValue = _initialEnergy;
        _currentTime = _timeAfkToRecoveryEnergy;

        _greenEnergySlider.maxValue = _initialValueGreenEnergy;
        _currentGreenEnergy = 0;

        _typeEnergy = EnergysTypes.Normal;

        _tweenManager = FindObjectOfType<TweenManager>();
        DOTween.Init();        
    }
    
    void Update()
    {
        if (_normalEnergyIsActivated) { _typeEnergy = EnergysTypes.Normal; }

        if(_greenEnergyIsActivated && _normalEnergyIsActivated == false) { _typeEnergy = EnergysTypes.Green; }

        switch (_typeEnergy) 
        {
            case EnergysTypes.Normal:

                _activatedSliderEnergy.Invoke();
                _desactivatedSliderGreenEnergy.Invoke();

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

                if (_currentEnergy <= 0)
                {
                    _onEnergyEndsEvent.Invoke();
                    _regeneratingEnergy = true;
                    _currentEnergy = _initialEnergy;
                    StartCoroutine(RegeneratingAllEnergy());
                }
                break;

            case EnergysTypes.Green:

                _desactivatedSliderEnergy.Invoke();
                _activatedSliderGreenEnergy.Invoke();
                
                if(_currentGreenEnergy <= 0) { _normalEnergyIsActivated = true; }
                else { _greenEnergyIsActivated = true; }

                break;
        }       
    }

    #region Normal Energy
    public void RegenerateEnergy(float value)
    {
        float num = _currentEnergy + value;
        if (num <= _initialEnergy)
        {
            _currentEnergy += value;
            _energySlider.DOValue(_currentEnergy, 1);
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
        float num = _currentEnergy - value;
        if (num >= 0)
        {
            _currentEnergy -= value;
            _energySlider.DOValue(_currentEnergy, 1);
            _onEnergyChangedEvent.Invoke();
            RestarCounterEnergy();
        }
        else
        {
            num = 0;
            Debug.Log("No se puede reducir más la energia");
        }
    }

    public float ConsultCurrentEnergy() // Method to obtained the curren Energy 
    {
        return _currentEnergy;
    }
    IEnumerator RegeneratingAllEnergy()  // Method to regenerate all the energy
    {
        _energySlider.value = 0f;
        _energySlider.DOValue(_initialEnergy, _timeToRechargeAllEnergy);
        _tweenManager.TweenRegenerateAllEnergy();
        _regeneratingEnergy = true;
        yield return new WaitForSeconds(_timeToRechargeAllEnergy);
        _regeneratingEnergy = false;
        RestarCounterEnergy();
    }

    public void ReduceRunEnergy() // METHOD TO REDUCE ENERGY WHILE THE PLAYER RUNS
    {
        if (_currentEnergy != 0 && _regeneratingEnergy == false)
        {
            _currentEnergy -= _costRunEnergy * Time.deltaTime;
            _energySlider.value = _currentEnergy;
            RestarCounterEnergy();
        }
    }

    public void IncrementEnergy()  //Method to increase energy over time
    {
        _currentEnergy += _speedAbsorbEnergy * Time.deltaTime;
        _energySlider.value = _currentEnergy;
        RestarCounterEnergy();
    }

    public void RegeneratingPasiveEnergy() // Method for the player to enter a state of recovering energy
                                           // (It is different from regenerating all energy since it is dependent on a counter)
    {
        if (_currentEnergy <= _initialEnergy)
        {
            _currentEnergy += _SpeedRecoveryEnergyPassive * Time.deltaTime;
            _energySlider.value = _currentEnergy;
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
    #endregion

    #region Green Energy
    public float ConsultCurrentGreenEnergy()
    {
        return _currentGreenEnergy;
    }

    public void AbsorbGreenEnergy()
    {
        if (_currentGreenEnergy <= _initialValueGreenEnergy)
        {
            _currentGreenEnergy += _speedAbsorbGreenEnergy * Time.deltaTime;
            _greenEnergySlider.value = _currentGreenEnergy;
        }
    }

    public void DegeneratePassiveGreenEnergy()
    {
        if (_currentGreenEnergy >= 0)
        {
            _currentGreenEnergy -= _speedDecrementGreenEnergy * Time.deltaTime;
            _greenEnergySlider.value = _currentGreenEnergy;
        }
    }

    public void ReduceGreenEnergy(float value)
    {
        float num = _currentGreenEnergy - value;
        if (num >= 0)
        {
            _currentGreenEnergy -= value;
            _greenEnergySlider.DOValue(_currentGreenEnergy, 1f);
        }
        else
        {
            num = 0;
            Debug.Log("No se puede reducir más la energia");
        }
    }
    #endregion
}
