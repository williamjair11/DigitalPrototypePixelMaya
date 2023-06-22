using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using DG.Tweening;
using UnityEngine.Events;

public class TurnOnOffLight : MonoBehaviour
{
    
    [SerializeField] public EnergyController.EnergyTypes _funtionalEnergy;

    [SerializeField] private UnityEvent<float> _energyTorchOnEvent;

    private bool PlayerInZone;

    private InputController _inputController;

    private EnergyController _energyController;

    public bool _torchTurnedOn = false;

    [SerializeField] private GameObject txtToDisplay;

    #region
    [SerializeField] private Light _torch;
    [SerializeField] private float _speedLight;
    [SerializeField] private float _intensityLightOn;
    [SerializeField] private float _intensityLightOff;
    [SerializeField] private float _intensityLightMiddle;
    [SerializeField] private float _costTurnOnTorch;
    
    #endregion
    private void Start()
    {
        _inputController = FindObjectOfType<InputController>();
        _energyController = FindObjectOfType<EnergyController>();
        PlayerInZone = false;
        txtToDisplay.SetActive(false);

        if (_torchTurnedOn) { TorchOn(); }
    }

    private void Update()
    {

        if(_funtionalEnergy == EnergyController.EnergyTypes.White) { _torch.color = Color.white; }
        if (_funtionalEnergy == EnergyController.EnergyTypes.Green) { _torch.color = Color.green; }

        bool stateButtonControl = _inputController.Interact();

        if (PlayerInZone && _inputController.Interact() && _energyController._energyType == _funtionalEnergy && _torchTurnedOn == false && _energyController._regeneratingEnergy == false)
        {                            
           if(_energyController.ConsultCurrentEnergy() >= _costTurnOnTorch) 
            {
                TorchOn();
                _energyTorchOnEvent.Invoke(_costTurnOnTorch);
            }                                                 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") //player in zone
        {
            txtToDisplay.SetActive(true);
            PlayerInZone = true;
        }
    }


    private void OnTriggerExit(Collider other)  //player exits zone
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInZone = false;
            txtToDisplay.SetActive(false);
        }
    }

    #region Lights Tween
    public void TorchOn()
    {
        _torch.DOIntensity(_intensityLightOn, _speedLight);
        _torchTurnedOn = true;
    }

    public void TorchOff()
    {
        _torch.DOIntensity(_intensityLightOff, _speedLight);
        _torchTurnedOn = false;
    }

    public void TorchMiddle()
    {
        _torch.DOIntensity(_intensityLightMiddle, _speedLight);
        _torchTurnedOn = true;
    }
    #endregion
}
