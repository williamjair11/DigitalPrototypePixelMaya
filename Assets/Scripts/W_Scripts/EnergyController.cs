using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class EnergyController : MonoBehaviour
{
    [Header("General variables")]
    [SerializeField] private float _rateIncreaseSpeedEnergy;
    [SerializeField] private float _timeAfkToRecoveryEnergy;
    private bool _counterActivate = false;  
    private float _currentTime;
    public bool _regeneratingEnergy = false;

    [Header("statistics player")]
    [SerializeField] public float _initialEnergy;
    [SerializeField] public float _currentEnergy;
    [SerializeField] public float _timeToRechargeAllEnergy;
    [SerializeField] private float _costRunEnergy;

    [Header("Events")]
    [SerializeField] private UnityEvent _onEnergyFullEvent;
    [SerializeField] private UnityEvent _onEnergyChangedEvent;
    [SerializeField] private UnityEvent _onEnergyEndsEvent;
    [SerializeField] private UnityEvent _regeneratingEnergyEvent;

    [Header("Energy Bar references")]
    [SerializeField] Slider _energySlider;

    [Header("General References")]
    private TweenManager _tweenManager;
    void Start()
    {
        _currentEnergy = _initialEnergy;
        _energySlider.maxValue = _initialEnergy;
        _currentTime = _timeAfkToRecoveryEnergy;
        _tweenManager = FindObjectOfType<TweenManager>();
        DOTween.Init();
        
    }
    
    void Update()
    {
        if (_regeneratingEnergy == false) { _currentTime -= Time.deltaTime; }
        
        if( _currentTime <= 0) 
        {
            _counterActivate = true;
        }
        else 
        {
            _counterActivate = false;
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
    }

    public void RegenerateEnergy(float value) 
    {
        float num = _currentEnergy + value;
        if(num <= _initialEnergy) 
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

    public void ReduceEnergy(float value)
    {
        float num = _currentEnergy - value;
        if(num >= 0) 
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

    public float ConsultCurrentEnergy()
    {
        return _currentEnergy;
    }
    IEnumerator RegeneratingAllEnergy() 
    {
        _energySlider.value = 0f;
        _energySlider.DOValue(_initialEnergy, _timeToRechargeAllEnergy);
        _tweenManager.TweenRegenerateAllEnergy();
        _regeneratingEnergy = true;
        yield return new WaitForSeconds(_timeToRechargeAllEnergy);
        _regeneratingEnergy = false;
        RestarCounterEnergy();
    }

    public void ReduceRunEnergy()
    {
        if (_currentEnergy != 0 && _regeneratingEnergy == false) 
        {
            _currentEnergy -= _costRunEnergy * Time.deltaTime;
            _energySlider.value = _currentEnergy;
            RestarCounterEnergy();
        }
    }

    public void RegeneratingPasiveEnergy() 
    {
        if(_currentEnergy <= _initialEnergy) 
        {
            _currentEnergy += _rateIncreaseSpeedEnergy * Time.deltaTime;
            _energySlider.value = _currentEnergy;
        }
        else 
        {
            _currentTime = _timeAfkToRecoveryEnergy;
            DesactivateCounterEnergy();
        }
    }

    public void RestarCounterEnergy() 
    {
        _currentTime = _timeAfkToRecoveryEnergy;
    }

    public void ActivateCounterEnergy() 
    {
        _counterActivate = true;
    }

    public void DesactivateCounterEnergy() 
    {
        _counterActivate = false;
    }
}
