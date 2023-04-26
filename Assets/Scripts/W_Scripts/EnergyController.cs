using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class EnergyController : MonoBehaviour
{       
    [SerializeField]
    private float _initialEnergy;

    [SerializeField]
    public float _currentEnergy;

    [SerializeField]
    private UnityEvent _onEnergyFull;

    [SerializeField]
    private UnityEvent _onEnergyChanged;

    [SerializeField]
    private UnityEvent _onEnergyEnds;

    [SerializeField]
    private Slider _energySlider;

    [SerializeField]
    private Image _filLContener;
    void Start()
    {
        _currentEnergy = _initialEnergy;
        _energySlider.maxValue= _initialEnergy;
        _energySlider.value = _currentEnergy;
    }
    private void Update()
    {
        if (_currentEnergy <= 70) { _filLContener.color = Color.yellow; }

        if (_currentEnergy <= 40) { _filLContener.color = Color.gray; }

        if (_currentEnergy <= 10) { _filLContener.color = Color.white; }
    }

    public void GetEnergy(float energyValue) 
    {
        
        _currentEnergy += energyValue;

        if (_currentEnergy >= _initialEnergy) 
        {
            _currentEnergy = _initialEnergy;
            _onEnergyFull.Invoke();
        }
        else 
        {           
            _onEnergyChanged.Invoke();
        }

        _energySlider.value = _currentEnergy;
    }

    public void SetEnergy(float energyValue)
    {
        _currentEnergy -= energyValue;

        if (_currentEnergy <= 0)
        {
            _currentEnergy= 0;
            _onEnergyEnds.Invoke();
        }
        else
        {
            _onEnergyChanged.Invoke();
        }
        _energySlider.value = _currentEnergy;
    }

    public float GetCurrentEnergy() 
    {
        return _currentEnergy;
    }

    public void IncreaseEnergy() 
    {
            
    }

    public void DecreaseEnergy() 
    {
        
    }
}
