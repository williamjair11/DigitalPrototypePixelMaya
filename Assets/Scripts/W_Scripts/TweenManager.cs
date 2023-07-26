using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;

public class TweenManager : MonoBehaviour
{
    [SerializeField] private GameObject _sliderObject;
    [SerializeField] private Image _sliderImage;
    [SerializeField] private Image _sliderNormalEnergy;

    [SerializeField] private Camera _camera;

    [SerializeField] private Light _lightPlayer;

    private EnergyController _energyController;
    private WhiteEnergy _whiteEnergy;

    void Start()
    {
        DOTween.Init();
        _energyController = FindObjectOfType<EnergyController>();
        _whiteEnergy = FindObjectOfType<WhiteEnergy>();
    }

    #region Slider normal energy Tween


    public void TweenFlashHabilitySlider() 
    {
        _sliderObject.transform.DOShakeScale(1f, 1f, 20, 90);
        _sliderImage.color = Color.green;
        _sliderImage.DOColor(Color.white, 5f);
    }

    public void TweenInsufficientEnergySlider() 
    {
        Sequence _sequence = DOTween.Sequence();
        _sequence.Insert(0, _sliderImage.DOColor(Color.red, 0.1f));
        _sequence.Insert(1, _sliderImage.DOColor(Color.white, 0.1f));       
    }

    public void TweenRegenerateAllEnergy() 
    {
        float timeRecovery = _whiteEnergy._timeToRechargeAllEnergy;
        Sequence _sequence = DOTween.Sequence();

        _sequence.Insert(0, _sliderImage.DOColor(Color.blue, timeRecovery));
        _sequence.Append(_sliderImage.DOColor(Color.white, 0.5f));
    }
    #endregion


    #region Camera Tween
    public void ShakeCameraForReciveDamage() 
    {
        
    }
    #endregion
}
