using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System.Runtime.InteropServices;

[RequireComponent(typeof(Interruptor))]
public class fireMechanism : InteractiveObject
{
    [SerializeField] private bool _isActive = false;
    [SerializeField] private EnergyController.EnergyType _energyType = EnergyController.EnergyType.White;
    [SerializeField] private Interruptor _interruptor;
    void Awake()
    {
        if(!_interruptor) _interruptor = GetComponent<Interruptor>();
    }
    
    public void SetEnergyType()
    {
        GameManager.Instance.playerController.EnergyController.SetEnergyType(_energyType);
    }

    public override void Interact()
    {
        if(!_playerCanInteract) return;
        base.Interact();
        _isActive = !_isActive;
        if(_isActive && GameManager.Instance.playerController.EnergyController.CurrentEnergyType == _energyType) 
        _interruptor._onActive?.Invoke(); 
        else
        {
            _isActive =false;
            _interruptor._onDesactive?.Invoke();
            if(_interruptor.IsActive) SetEnergyType();
        }
        
        _interruptor.IsActive = _isActive;
        _interruptor._onChange?.Invoke();
    }
}
