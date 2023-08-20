using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class HudController : MonoBehaviour
{
    [SerializeField] Slider _energySlider;
    GameObject _shortInventoryMenu;
    [SerializeField] GameObject _heartPrefab, _heartsContainer;
    [SerializeField] HealthController _playerHealtController;
    [SerializeField] Image _energyFill;
    int _heartsCount = 0;
    private bool _updatingEnergy = false;
    float _targetEnergy = 0;

    public void UpdateHearts()
    {
        _heartsCount = _playerHealtController.CurrentHealt;
        if(_heartsContainer.transform.childCount < _heartsCount)
        AddHearts();
        else
        RemoveHearts();
    }

    void AddHearts()
    {
        int secure = 0;
        while(_heartsContainer.transform.childCount < _heartsCount)
        {
            GameObject newHeart = Instantiate(_heartPrefab, _heartsContainer.transform);
            if(secure > 100) break;
        }
    }

    void RemoveHearts()
    { int counter = 0;
        foreach(Transform child in _heartsContainer.transform)
        {
            if(_heartsContainer.transform.childCount -_heartsCount <= counter) break;
            child.transform.DOScale(0,1f);
            child.GetComponent<DestroyWithDelay>().Destroy(1f);
            counter++;
        }
    }

    public void UpdateEnergy(float energy)
    {
        float animDuration = .1f;
        if(Mathf.Abs(_energySlider.value - energy) >= .10f) animDuration = 1;
        _updatingEnergy = true;
        _energySlider.DOValue(energy, animDuration);  
    }

    public void UpdateEnergyColor(Color energyColor)
    {
        _energyFill.DOColor(energyColor, 1f);
    }
}
