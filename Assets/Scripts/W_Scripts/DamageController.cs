using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class DamageController : MonoBehaviour
{
    [SerializeField] protected int _damageAmount;
    [SerializeField] private bool _canMakeDamage;
    [SerializeField] public List<string> _targets = new List<string>();
    [SerializeField] private UnityEvent OnMakeDamage;
    private float _damageDelay = .5f, _timeCounter;
    private bool _waitingToMakeMoreDamage;
    public int DamageAmount
    {
        get => _damageAmount;
        set => _damageAmount = value;
    }

    void Update()
    {
        if(_waitingToMakeMoreDamage)
        {
            _canMakeDamage = false;
            _timeCounter += Time.deltaTime;
            if(_timeCounter >= _damageDelay)
            {
                _timeCounter = 0;
                _waitingToMakeMoreDamage = false;
            }
        }
    }
    public bool CanMakeDamage{get=> _canMakeDamage; set => _canMakeDamage = value;}
    public void OnTriggerEnter(Collider other)
    {
        if(_targets.Contains(other.tag) && CanMakeDamage && !_waitingToMakeMoreDamage)
        {
            _waitingToMakeMoreDamage = true;
            Debug.Log("Making Damage");
            HealthController targetHealthController =  other.gameObject.GetComponent<HealthController>();
            targetHealthController.ReciveDamage(_damageAmount);
            OnMakeDamage?.Invoke();
        }
    }
  
    public void setCanMakeDamageFalse()
    {
        _canMakeDamage = false;
    }
}
