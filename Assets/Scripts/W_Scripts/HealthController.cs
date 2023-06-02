using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class HealthController : MonoBehaviour
{
    enum objectType { Player, Enemy }
    [SerializeField] objectType _objectType;

    [SerializeField] private float _initialHealt;    

    [SerializeField] private UnityEvent _OnReciveDamage;

    [SerializeField] private UnityEvent _OnDie;

    [SerializeField] private UnityEvent _OnRestoreHealt;

    [SerializeField] private UnityEvent _OnRestoreMaximumHealt;

    [SerializeField] Slider _healtSlider;

    [SerializeField] private float _currentHealt;

    

    private void Start()
    {
        _currentHealt = _initialHealt;
        DOTween.Init();
    }
    public void ReciveDamage(float damage, string target) 
    {      
        float num = _currentHealt - damage;
       
        if( num >= 0) 
        {
            _currentHealt -= damage;
            Debug.Log("Recive Damage: " + damage + " Current healt is; " + _currentHealt + " Object:" + target);
            if(_objectType == objectType.Player) { _healtSlider.DOValue(_currentHealt, 1); }
            
            _OnReciveDamage.Invoke(); //insert methods animations and sounds      
        }
        else 
        {
            _OnDie.Invoke();
            if (_objectType == objectType.Player) { _healtSlider.DOValue(_currentHealt, 1); }
            
            if (_objectType == objectType.Enemy) { Destroy(this.gameObject); }
            
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
            if (_objectType == objectType.Player) { _healtSlider.DOValue(_currentHealt, 1); }
        }
        else 
        {
            _currentHealt = _initialHealt;
            _OnRestoreMaximumHealt.Invoke();
            if (_objectType == objectType.Player) { _healtSlider.DOValue(_currentHealt, 1); }
            Debug.Log("Full healt");
        }
    }
}
