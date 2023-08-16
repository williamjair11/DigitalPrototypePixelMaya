using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;
using System;

//TODO: usar unityEvents o actions para hacer que los cambios de energía se ejecuten uno tras otro
//hacer que este script solo se encargue de llevar la cuenta de la energía y que el hud se encargue de
//representar visualmente los cambios
public class EnergyController : MonoBehaviour
{
    public enum EnergyType { White, Green}
    [Serializable]
    public struct EnergyData
    {
            public EnergyType type;
            public int damageMultiplier;
            public float wasteForSecond;
            public float recoveryForSecond;
            public float isInfinite;
    }

    
    private EnergyData _currentEnergyData;
    private float _currentEnergy = 0;
    private bool _updatingEnergy;
    [SerializeField] private float _initialEnergy = 1, _maxEnergy = 1;
    [SerializeField] private List<EnergyData> _energyList;
    [SerializeField] UnityEvent<float> _onChangeCurrentEnergy;

    public bool UpdatingEnergy { get => _updatingEnergy;}
    public float CurrentEnergy { get => _currentEnergy;}
    public void Start()
    {
        SetEnergyType();
        IncreaseEnergy(_initialEnergy);
    }

    public void IncreaseEnergy(float energyAmount)
    {
        _currentEnergy += energyAmount;
        if(_currentEnergy > _maxEnergy) _currentEnergy = _maxEnergy;
            _onChangeCurrentEnergy?.Invoke(_currentEnergy);
    }


    public void DecreaseEnergy(float energyAmount)
    {
            _currentEnergy -= energyAmount;
            if(_currentEnergy <= 0)
            {
                if(_currentEnergyData.type != EnergyType.White)
                SetEnergyType(EnergyType.White);
                _currentEnergy = 0;
            }
            _onChangeCurrentEnergy?.Invoke(_currentEnergy);
            if(_currentEnergyData.type == EnergyType.White)
            {
                if(_updatingEnergy)
                StopAllCoroutines();
                StartCoroutine(RegenerateEnergy());
            }
    }

    public void SetEnergyType(EnergyType energyType = EnergyType.White)
    {
        foreach (EnergyData item in _energyList)
        {
            if(item.type == energyType)
            {
                _currentEnergyData = item;
                break;
            }
        }
    }

    public IEnumerator RegenerateEnergy()
    {
        _updatingEnergy = true;
        yield return new WaitForSeconds(3);
        IncreaseEnergy(1);
        _updatingEnergy = false;
    }
}
