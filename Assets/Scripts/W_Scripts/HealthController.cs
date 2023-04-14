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
    private void Update()
    {
        
    }
    public void ReciveDamage(float damage, string target) 
    {      
        _currentHealt -= damage;

        if( _currentHealt <= 0) 
        {
            _currentHealt = 0;
            _OnDie.Invoke();
            Debug.Log("Dead");
        }
        else 
        {
            Debug.Log("Recive Damage: "+ damage + " Current healt is; " + _currentHealt + " Object:" + target);
            _OnReciveDamage.Invoke(); //insert methods animations and sounds
        }
        
        if(target == "Player") 
        {
            _healtSlider.value = _currentHealt;
        }      
    }

    public void OnRestoreHealt(float healtRestore, string target) 
    {
        _currentHealt += healtRestore;

        if (_currentHealt >= _initialHealt) 
        {
            _currentHealt=_initialHealt;
            _OnRestoreMaximumHealt.Invoke();
            Debug.Log("Full healt");
        }
        else 
        {
            Debug.Log("Restore healt: " + healtRestore + " Current healt is; " + _currentHealt);
            _OnRestoreHealt.Invoke(); //insert methods animations and sounds
        }

        if (target == "Player")
        {
            _healtSlider.value = _currentHealt;
        }    
    }
}
