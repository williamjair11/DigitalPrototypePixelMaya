using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GreenEnergy : MonoBehaviour
{
    [SerializeField] public float _initialEnergy;

    [NonSerialized] public float _currentValueEnergy;

    [SerializeField] public float _speedAbsorbEnergy;

    [SerializeField] public float _costRunEnergy;

    [Header("Energy Bar references")]
    [SerializeField] private Slider _energySlider;
    [SerializeField] private GameObject _energyContainer;

    [Header("Twween manager References")]
    private TweenManager _tweenManager;

    private EnergyController _energyController;

    [Header("Events Normal Energy")]
    [SerializeField] private UnityEvent _onEnergyFullEvent;
    [SerializeField] private UnityEvent _onEnergyChangedEvent;
    [SerializeField] private UnityEvent _onEnergyEndsEvent;
    [SerializeField] private UnityEvent _regeneratingEnergyEvent;

    private void Start()
    {
        _currentValueEnergy = 0;
        _energySlider.maxValue = _initialEnergy;
        _tweenManager = FindObjectOfType<TweenManager>();
        _energyController = FindObjectOfType<EnergyController>();
        DOTween.Init();      
    }

    private void Update()
    {
        if (_energyController._energyType == EnergyController.EnergyTypes.Green) { _energyContainer.SetActive(true); }
        else { _energyContainer.SetActive(false); }  
        
        if(_currentValueEnergy < 0) { _energyController.ReturnToTheDefaultEnergy(); }
    }

    public void RegenerateEnergy(float value) //Method to increment a certain value of energy
    {
        float num = _currentValueEnergy + value;
        if (num <= _initialEnergy)
        {
            _currentValueEnergy += value;
            _energySlider.DOValue(_currentValueEnergy, 1);
            _onEnergyChangedEvent.Invoke();
        }
        else
        {
            num = 0;
            //Debug.Log("No se puede sobrepasar el nivel de energía");
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
        }
        else
        {
            num = 0;
            //Debug.Log("No se puede reducir más la energia");
        }
    }

    public void ReduceRunEnergy() // METHOD TO REDUCE ENERGY WHILE THE PLAYER RUNS
    {
        if (_currentValueEnergy >= 0)
        {
            _currentValueEnergy -= _costRunEnergy * Time.deltaTime;
            _energySlider.value = _currentValueEnergy;

        }
    }

    public void IncrementEnergy()  //Method to increase energy over time
    {
        _currentValueEnergy += _speedAbsorbEnergy * Time.deltaTime;
        _energySlider.value = _currentValueEnergy;
    }

    public float ConsultCurrentEnergy() // Method to obtained the curren Energy 
    {
        return _currentValueEnergy;
    }
}
