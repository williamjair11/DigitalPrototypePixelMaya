using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbEnergy : MonoBehaviour
{
    enum EnergyType {Normal, Green}

    [SerializeField] EnergyType energyType;

    [SerializeField] private float speedAbsorb;

    private bool _playerInRange;

    private InputController _inputController;

    private EnergyController _energyController;

    private GreenEnergy _greenEnergy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") { _playerInRange = true; }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") { _playerInRange = false; }
    }
    void Start()
    {
        _inputController = FindObjectOfType<InputController>();
        _energyController = FindObjectOfType<EnergyController>();
        _greenEnergy = FindObjectOfType<GreenEnergy>();
    }

    // Update is called once per frame
    void Update()
    {
        bool stateButton;
        if(_inputController._interact.IsPressed()) { stateButton = true; }
        else { stateButton = false; }

        switch (energyType) 
        {
            case EnergyType.Normal:
                if (_playerInRange && stateButton && _energyController.ConsultCurrentEnergy()<=100)
                {
                    float currentEnergy = _energyController.ConsultCurrentEnergy();
                    _energyController.IncrementEnergy();
                }
                break;

            case EnergyType.Green:
                if (_playerInRange && stateButton && _greenEnergy.ConsultCurrentEnergy() <= 100)
                {
                    float currentEnergy = _energyController.ConsultCurrentEnergy();
                    _greenEnergy.IncrementEnergy();
                }
                break;
        }       
    }
}
