using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    void Start()
    {
        _currentEnergy = _initialEnergy;
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
}
