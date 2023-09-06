using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpecialAttack : InventoryObject
{

    UnityEvent _onPerformAttack;
    [SerializeField] private float _wasteOfEnergy, _damagePoints;

    public float WasteOfEnergy { get => _wasteOfEnergy; }

    public virtual void PerformAttack()
    {
        
    }

}
