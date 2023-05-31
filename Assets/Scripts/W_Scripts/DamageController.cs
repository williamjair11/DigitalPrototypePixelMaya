using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class DamageController : MonoBehaviour
{
    [SerializeField] public float _damageAmount;

    [SerializeField] public bool _canMakeDamage;

    [SerializeField]
    private UnityEvent<float, string> OnMakeDamagePlayer;

    [SerializeField]
    private UnityEvent<float , string> OnMakeDamageEnemy;
    
    private void OnTriggerEnter(Collider other)
    {
        if (_canMakeDamage) 
        {
            OnMakeDamagePlayer.Invoke(_damageAmount, other.tag);
        }           
    }
  
    public void DesactivatedCanMakeDamage() 
    {
        _canMakeDamage = false;
    }

    public void ActivatedCanMakeDamage() 
    {
        _canMakeDamage = true;
    }
}
