using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class HealtController : MonoBehaviour
{
    private float _currentHealt;

    [SerializeField] private float 
    _initialHealt = 100, 
    _maxHealt = 100,
    _minHealt = 0,
    _damageImnunityTime = 3;

    bool _canReciveDamage = true;

    [SerializeField] private UnityEvent OnRecoverHealt, OnLostHealt;
    // Start is called before the first frame update

    public float CurrentHealt {get => _currentHealt;}
    public float MaxHealt{get => _maxHealt;}

    void Start()
    {
        _currentHealt = _initialHealt;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecoverHealt(float healt)
    {
        if(_currentHealt < _maxHealt)
        {
            _currentHealt += healt;
            OnLostHealt?.Invoke();
        }
        
    }

    public void DecreaseHealt(float damage)
    {
        if(_currentHealt > _minHealt)
        {
            _currentHealt -= damage;
            OnRecoverHealt?.Invoke();
        }
    }

    IEnumerator DamageImmunity()
    {
        _canReciveDamage = false;
        yield return new WaitForSeconds(_damageImnunityTime);
        _canReciveDamage = true;
    }
}
