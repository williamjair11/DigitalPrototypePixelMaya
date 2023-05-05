using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnergyController : MonoBehaviour
{
    [Header("General variables")]
    private bool _regenerateEnergy=false;
    private bool _decreaseEnergy = false;
    private bool _regenerateAllEnergy = false;
    private float _valueLossEnergy=0;
    private float _valueIncreaseEnergy=0;
    
    [SerializeField] private float _rateIncreaseSpeedEnergy;

    [Header("statistics player")]
    [SerializeField] private float _initialEnergy;
    [SerializeField] public float _currentEnergy;

    [Header("Events")]
    [SerializeField] private UnityEvent _onEnergyFull;
    [SerializeField] private UnityEvent _onEnergyChanged;
    [SerializeField] private UnityEvent _onEnergyEnds;

    [Header("Energy Bar references")]
    [SerializeField] private Slider _energySlider;
    void Start()
    {
        _currentEnergy = _initialEnergy;
        _energySlider.maxValue = _initialEnergy;
    }

    
    void Update()
    {
        if (_currentEnergy <= 0) { _regenerateAllEnergy=true; }

        if (_regenerateAllEnergy)
        {
            _currentEnergy += _rateIncreaseSpeedEnergy * Time.deltaTime;
            _energySlider.value = _currentEnergy;

            if (_currentEnergy >= _initialEnergy)
            {
                _regenerateAllEnergy = false;
                _currentEnergy = _initialEnergy;
            }
            //_energySlider.value = _initialEnergy;
        }

        if (_regenerateEnergy) 
        {        
                _currentEnergy += _rateIncreaseSpeedEnergy * Time.deltaTime;
                _energySlider.value = _currentEnergy;
            
            if(_currentEnergy >= _valueIncreaseEnergy) 
            {
                _regenerateEnergy = false;
                _currentEnergy = _valueIncreaseEnergy;
            }
            //_energySlider.value = _initialEnergy;
        }

        if (_decreaseEnergy)
        {
            _currentEnergy -= 23f * Time.deltaTime;
            _energySlider.value = _currentEnergy;

            if (_currentEnergy <= _valueLossEnergy)
            {
                _decreaseEnergy = false;
                _currentEnergy = _valueLossEnergy;
            }
            //_energySlider.value = _valueLossEnergy;
        }
    }

    public void GetEnergy(float energyValue) 
    {
        _valueIncreaseEnergy = _currentEnergy + energyValue;

        if (_currentEnergy >= _initialEnergy) 
        {
            _currentEnergy = _initialEnergy;
            _onEnergyFull.Invoke();
        }
        else 
        {           
            _onEnergyChanged.Invoke();
            regenerateEnergy();
        }     
    }

    public void SetEnergy(float energyValue)
    {
        _valueLossEnergy = _currentEnergy - energyValue;

        if (_valueLossEnergy <= 0)
        {
            _currentEnergy= 0;
            _onEnergyEnds.Invoke();
        }
        else
        {      
            _onEnergyChanged.Invoke();
            decreaseEnergy();
        }      
    }

    public void regenerateEnergy() 
    {
        _regenerateEnergy = true;
    }
    
    public void decreaseEnergy() 
    {
        _decreaseEnergy = true;
    }

    public void regenerateAllEnergy() 
    {
        _regenerateAllEnergy = true;
    }
}
