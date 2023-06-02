using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;


public class EnergyController : MonoBehaviour
{
    [Header("General variables")]
    [SerializeField] private float _rateIncreaseSpeedEnergy;
    private bool _regeneratingEnergy = false;

    [Header("statistics player")]
    [SerializeField] public float _initialEnergy;
    [SerializeField] public float _currentEnergy;

    [Header("Events")]
    [SerializeField] private UnityEvent _onEnergyFullEvent;
    [SerializeField] private UnityEvent _onEnergyChangedEvent;
    [SerializeField] private UnityEvent _onEnergyEndsEvent;
    [SerializeField] private UnityEvent _regeneratingEnergyEvent;

    [Header("Energy Bar references")]
    [SerializeField] Slider _energySlider;
    void Start()
    {
        _currentEnergy = _initialEnergy;
        _energySlider.maxValue = _initialEnergy;
        DOTween.Init();

    }
    
    void Update()
    {
        if (_currentEnergy <= 0) 
        {
            _onEnergyEndsEvent.Invoke();
            _regeneratingEnergy = true;
            RegenerateEnergy(_initialEnergy);           
        }

        if (_regeneratingEnergy) { _regeneratingEnergyEvent.Invoke(); }
    }

    public void RegenerateEnergy(float value) 
    {
        float num = _currentEnergy + value;
        if(num <= _initialEnergy) 
        {
            _currentEnergy += value;
            _energySlider.DOValue(_currentEnergy, 1);
            _onEnergyChangedEvent.Invoke();
            _regeneratingEnergy = false;
        }
        else 
        {
            num = 0;
            Debug.Log("No se puede sobrepasar el nivel de energía");
        }
    }

    public void ReduceEnergy(float value)
    {
        float num = _currentEnergy - value;
        if(num >= 0) 
        {
            _currentEnergy -= value;
            _energySlider.DOValue(_currentEnergy, 1);
            _onEnergyChangedEvent.Invoke();
        }
        else 
        {
            num = 0;
            Debug.Log("No se puede reducir más la energia");
        }
    }

    public float ConsultCurrentEnergy()
    {
        return _currentEnergy;
    }
}
