using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField] int _initialHealt = 5, _maxHealt = 5, _minHealt = 1;
    [SerializeField] private int _currentHealt, _timeToRecover = 3;
    public int CurrentHealt{get => _currentHealt;}
    [SerializeField] UnityEvent _onIncreaseHealt, _onReciveDamage, _onDie;
    [SerializeField] private bool _recoveringFromDamage, _canReciveDamage = true;
    [SerializeField] private float _timeCounter = 0;

    void Start()
    {
        IncreaseHealt(_initialHealt);
    }

    void Update()
    {
        if(_recoveringFromDamage)
        {
            _canReciveDamage = false;
            _timeCounter += Time.deltaTime;
            if(_timeCounter >= _timeToRecover)
            {
                _timeCounter = 0;
                _recoveringFromDamage = false;
                _canReciveDamage = true;
            }
        }
    }
    public void IncreaseHealt(int healtAmount)
    {
        _currentHealt = healtAmount + _currentHealt > _maxHealt? 
        _maxHealt : _currentHealt + healtAmount;
        _onIncreaseHealt?.Invoke();
    }

    public void ReciveDamage(int healtAmount)
    {
        if(!_canReciveDamage) return;
        _recoveringFromDamage = true;
        _currentHealt -= healtAmount;
        if(_currentHealt < _minHealt)
        {
            _onDie?.Invoke();
        }
        _onReciveDamage?.Invoke();
        
    }


}
