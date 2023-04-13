using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    private UnityEvent _OnRestoreMaximimHealt;

    private float _currentHealt;

    private void Start()
    {
        _currentHealt = _initialHealt;
    }
    private void Update()
    {
        if (_currentHealt <= 0) 
        {
            OnDie(); 
            _currentHealt=0;
        }
    }
    public void ReciveDamage(float damage) 
    {      
        _currentHealt -= damage;
        Debug.Log("Make damage: " + damage + "Vida actual: " + _currentHealt);
        _OnReciveDamage.Invoke(); //insert methods animations and sounds
    }

    public void OnRestoreHealt(float healtRestore) 
    {
        _currentHealt = healtRestore;
        Debug.Log("Healt restore" + healtRestore + "Vida actual: " + _currentHealt);
        _OnRestoreHealt.Invoke(); //insert methods animations and sounds
    }

    public void OnRestoreMaximumHealt() 
    { 
        _currentHealt = _initialHealt;
        Debug.Log("Healt restore" + _initialHealt + "Vida actual: " + _currentHealt );
        _OnRestoreMaximimHealt.Invoke(); //insert methods animations and sounds
    }
    public void OnDie() 
    {
        Debug.Log("Game over");
        _OnDie.Invoke(); //insert methods animations and sounds
    }  
}
