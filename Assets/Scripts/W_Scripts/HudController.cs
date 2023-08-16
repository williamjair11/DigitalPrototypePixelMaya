using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HudController : MonoBehaviour
{
    [SerializeField] Slider _energySlider;
    GameObject _shortInventoryMenu;
    [SerializeField] GameObject _heartPrefab, _heartsContainer;
    [SerializeField] HealthController _playerHealtController;
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
    {
        foreach(Transform child in _heartsContainer.transform)
        {
            if(_heartsCount == _heartsContainer.transform.childCount) break;

            child.GetComponent<ResizeUIImage>().MakeSmall();
        }
    }

    public void UpdateEnergy(float energy)
    {
        
        if(_updatingEnergy)
            StopAllCoroutines();
        

        if(Mathf.Abs(_energySlider.value - energy) >= .10f && gameObject.activeSelf)
        {
                _updatingEnergy = true;
                StartCoroutine(AnimateEnergyBar(energy));
        }
        else
        _energySlider.value = energy;
        
    }

    public IEnumerator AnimateEnergyBar(float targetEnergy)
    {
        while(_energySlider.value != targetEnergy)
        {
            yield return new WaitForSeconds(.2f);
            if(targetEnergy > _energySlider.value)
                _energySlider.value += .05f;
            else
                _energySlider.value -= .05f;
        }
        _updatingEnergy = false;
    }
}
