using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GreenEnergy : MonoBehaviour
{
    [Header("General variables green Energy")]
    [SerializeField] Slider _greenEnergySlider;
    [SerializeField] private float _speedAbsorbGreenEnergy;
    [NonSerialized] public float _currentGreenEnergy;
    [SerializeField] private float _initialValueGreenEnergy;
    [NonSerialized] public bool _greenEnergyIsActivated;

    [SerializeField] private UnityEvent _desactivatedSliderGreenEnergy;
    [SerializeField] private UnityEvent _activatedSliderGreenEnergy;
    void Start()
    {
        _currentGreenEnergy = 0;
        _greenEnergySlider.maxValue = _initialValueGreenEnergy;
    }
    void Update()
    {
        _currentGreenEnergy -= 1.5f * Time.deltaTime;
        _greenEnergySlider.value = _currentGreenEnergy;

        if (_currentGreenEnergy > 1)
        {
            _greenEnergyIsActivated = true;
            _activatedSliderGreenEnergy.Invoke();
        }
        else
        {
            _greenEnergyIsActivated = false;
            _desactivatedSliderGreenEnergy.Invoke();
        }
    }
    #region

    public float ConsultCurrentEnergy()
    {
        return _currentGreenEnergy;
    }

    public void IncrementEnergy()
    {
        _currentGreenEnergy += _speedAbsorbGreenEnergy * Time.deltaTime;
        _greenEnergySlider.value = _currentGreenEnergy;
    }
    public void AbsorbGreenEnergy()
    {
        if (_currentGreenEnergy <= _initialValueGreenEnergy)
        {
            _currentGreenEnergy += _speedAbsorbGreenEnergy * Time.deltaTime;
            _greenEnergySlider.value = _currentGreenEnergy;
        }
    }

    public void DegenerateGreenEnergy()
    {
        if (_currentGreenEnergy >= 0)
        {
            _currentGreenEnergy -= _speedAbsorbGreenEnergy * Time.deltaTime;
            _greenEnergySlider.value = _currentGreenEnergy;
        }
    }

    public void ReduceGreenEnergy(float value)
    {
        float num = _currentGreenEnergy - value;
        if (num >= 0)
        {
            _currentGreenEnergy -= value;
            _greenEnergySlider.DOValue(_currentGreenEnergy, 1);
        }
        else
        {
            num = 0;
            Debug.Log("No se puede reducir más la energia");
        }
    }
    #endregion
}
