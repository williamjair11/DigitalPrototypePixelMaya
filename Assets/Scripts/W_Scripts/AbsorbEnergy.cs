using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbEnergy : MonoBehaviour
{
    [SerializeField] Light _lightTorch;

    [SerializeField] private bool _isAbsorbable;

    [SerializeField] private float speedAbsorb;

    [SerializeField] private GameObject txtToDisplay;

    private bool _playerInRange;

    private InputController _inputController;

    private EnergyController _energyController;

    private TurnOnOffLight _turnOnOffLight;

  
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        { 
            _playerInRange = true;
            txtToDisplay.SetActive(true);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") 
        { 
            _playerInRange = false; 
           txtToDisplay.SetActive(false);
        }
    }
    void Start()
    {
        _inputController = FindObjectOfType<InputController>();
        _energyController = FindObjectOfType<EnergyController>();
        _turnOnOffLight = GetComponent<TurnOnOffLight>();
        txtToDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_inputController._interact.IsPressed() && _playerInRange && _turnOnOffLight._torchTurnedOn && _isAbsorbable) 
        {
            if(_turnOnOffLight._funtionalEnergy == EnergyController.EnergyTypes.White) 
            {
                _energyController._energyType = EnergyController.EnergyTypes.White;
                _energyController.AbsorbEnergy();
            }
            if (_turnOnOffLight._funtionalEnergy == EnergyController.EnergyTypes.Green)
            {
                _energyController._energyType = EnergyController.EnergyTypes.Green;
                _energyController.AbsorbEnergy();
            }
        }             
    }
}
