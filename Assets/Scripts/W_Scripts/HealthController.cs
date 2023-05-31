using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float _initialHealt;    
    [SerializeField]
    private UnityEvent _OnReciveDamage;
    [SerializeField]
    private UnityEvent _OnDie;
    [SerializeField]
    private UnityEvent _OnRestoreHealt;
    [SerializeField]
    private UnityEvent _OnRestoreMaximumHealt;
    [SerializeField]
    private Slider _healtSlider;
    [SerializeField]
    private float _currentHealt;

    private void Start()
    {
        _currentHealt = _initialHealt;
    }
    public void ReciveDamage(float damage, string target) 
    {      
        float num = _currentHealt - damage;
        
        if( num >= 0) 
        {
            _currentHealt -= damage;
            Debug.Log("Recive Damage: " + damage + " Current healt is; " + _currentHealt + " Object:" + target);
            _healtSlider.value = _currentHealt;
            _OnReciveDamage.Invoke(); //insert methods animations and sounds      
        }
        else 
        {
            _OnDie.Invoke();
            _healtSlider.value = _currentHealt;
            Debug.Log("Dead");
        }           
    }

    public void RestoreHealt(float healtRestore, string target) 
    {
        float num = _currentHealt + healtRestore;
       
        if (num <= _initialHealt) 
        {
            _currentHealt += healtRestore;
            Debug.Log("Restore healt: " + healtRestore + " Current healt is; " + _currentHealt + "for: " + target);
            _OnRestoreHealt.Invoke(); //insert methods animations and sounds
            _healtSlider.value = _currentHealt;
        }
        else 
        {
            _currentHealt = _initialHealt;
            _OnRestoreMaximumHealt.Invoke();
            _healtSlider.value = _currentHealt;
            Debug.Log("Full healt");
        }
    }
}
