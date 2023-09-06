using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System.Runtime.InteropServices;

[RequireComponent(typeof(Interruptor), typeof(SafeZone))]
public class fireMechanism : InteractiveObject
{
    [SerializeField] private bool _isActive = false;
    [SerializeField] private EnergyController.EnergyType _energyType = EnergyController.EnergyType.White;
    [SerializeField] private Interruptor _interruptor;
    [SerializeField] private SafeZone _safeZone;
    void Awake()
    {
        if(!_interruptor) _interruptor = GetComponent<Interruptor>();
    }

    void Start()
    {
        _safeZone.IsEnabled = _isActive;
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
        if(_isActive) 
            _interruptor._onActive?.Invoke(); 
        else
            _interruptor._onDesactive?.Invoke();
        
        _interruptor.IsActive = _isActive;
        _safeZone.IsEnabled = _isActive;
        _interruptor._onChange?.Invoke();
    }
}
