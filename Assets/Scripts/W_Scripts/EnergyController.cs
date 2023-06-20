using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System;

public class EnergyController : MonoBehaviour
{
    public enum EnergyTypes { White, Green}
    [SerializeField] public EnergyTypes _energyType;        
   
    [Header("Variables Energy")]
    private WhiteEnergy _whiteEnergy;
    private GreenEnergy _greenEnergy;

    public bool _regeneratingEnergy = false;

    private void Awake()
    {
        _whiteEnergy = FindObjectOfType<WhiteEnergy>();
        _greenEnergy = FindObjectOfType<GreenEnergy>();
        _energyType = EnergyTypes.White;
    }

    private void Update()
    {
        _regeneratingEnergy = _whiteEnergy._regeneratingEnergy;
    }

    public void ReduceEnergy(float value) 
    {
        switch (_energyType) 
        {
            case EnergyTypes.White:
                _whiteEnergy.ReduceEnergy(value);
                break;
            case EnergyTypes.Green:
                _greenEnergy.ReduceEnergy(value);
                break;
        }
    }

    public void RegenerateEnergy(float value) 
    {
        switch (_energyType)
        {
            case EnergyTypes.White:
                _whiteEnergy.RegenerateEnergy(value);
                break;
            case EnergyTypes.Green:
                _greenEnergy.RegenerateEnergy(value);
                break;
        }
    }

    public float ConsultCurrentEnergy() // Method to obtained the current Energy 
    {
        float value = 0;
        switch (_energyType)
        {
            case EnergyTypes.White:
                value = _whiteEnergy.ConsultCurrentEnergy();
                break;
            case EnergyTypes.Green:
                value = _greenEnergy.ConsultCurrentEnergy();
                break;
        }
        return value;
    }

    public void ReduceRunEnergy() // METHOD TO REDUCE ENERGY WHILE THE PLAYER RUNS
    {
        switch (_energyType)
        {
            case EnergyTypes.White:
                _whiteEnergy.ReduceRunEnergy();
                break;
            case EnergyTypes.Green:
                _greenEnergy.ReduceRunEnergy();
                break;
        }
    }

    public void AbsorbEnergy()  //Method to increase energy over time
    {
        switch (_energyType)
        {
            case EnergyTypes.White:
                _whiteEnergy.IncrementEnergy();
                break;
            case EnergyTypes.Green:
                _greenEnergy.IncrementEnergy();
                break;
        }
    }

    public void ReturnToTheDefaultEnergy() 
    {
        _energyType = EnergyTypes.White;
    }
}
