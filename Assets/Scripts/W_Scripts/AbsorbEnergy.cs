using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbEnergy : MonoBehaviour
{
    enum EnergyType {Normal, Green}

    [SerializeField] Light _lightTorch;

    [SerializeField] EnergyType energyType;

    [SerializeField] private float speedAbsorb;

    [SerializeField] private GameObject txtToDisplay;

    private bool _playerInRange;

    private InputController _inputController;

    private EnergyController _energyController;

  
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
        txtToDisplay.SetActive(false);
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
                _lightTorch.color = Color.white;

                if (_playerInRange && stateButton && _energyController.ConsultCurrentEnergy()<=_energyController._initialEnergy)
                {
                    _energyController.IncrementEnergy();
                }
                break;

            case EnergyType.Green:
                _lightTorch.color = Color.green;

                if (_playerInRange && stateButton && _energyController.ConsultCurrentGreenEnergy() <= _energyController._initialValueGreenEnergy)
                {
                    _energyController._greenEnergyIsActivated = true;
                    _energyController._normalEnergyIsActivated = false;
                    _energyController.AbsorbGreenEnergy();                    
                }
                break;
        }       
    }
}
