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

    public int DamageAmount
    {
        get => _damageAmount;
        set => _damageAmount = value;
    }
    void Start()
    {
    }
    public bool CanMakeDamage{get=> _canMakeDamage; set => _canMakeDamage = value;}
    public void SetCanMakeDamageFalse()
    { 
        _canMakeDamage = false;
    }

    public void SetDamageAmount()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if(_targets.Contains(other.tag) && CanMakeDamage)
        {
            HealthController targetHealthController =  other.gameObject.GetComponent<HealthController>();
            targetHealthController.ReciveDamage(_damageAmount);
            OnMakeDamage?.Invoke();
        }
    }
  
    
}
