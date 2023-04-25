using UnityEngine;
using UnityEngine.Events;

public class DamageController : MonoBehaviour
{
    [SerializeField]
    public float _damageAmount;
    
    [SerializeField]
    private UnityEvent<float, string> OnMakeDamage;


    public bool _canMakeDamage=true;
       
    private void OnTriggerEnter(Collider other)
    {    
        OnMakeDamage.Invoke(_damageAmount, other.tag);
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
