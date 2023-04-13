using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DamageController : MonoBehaviour
{
    [SerializeField]
    public float _damageAmount;
    [SerializeField]
    private UnityEvent<float> OnMakeDamage;

    public bool _canMakeDamage;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && _canMakeDamage) 
        {
            OnMakeDamage.Invoke(_damageAmount);
            Destroy(gameObject);
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
