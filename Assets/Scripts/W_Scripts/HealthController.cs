using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField] int _initialHealt = 5, _maxHealt = 5, _minHealt = 1;
    private int _currentHealt;
    public int CurrentHealt{get => _currentHealt;}
    [SerializeField] UnityEvent _onIncreaseHealt, _onReciveDamage, _onDie;

    void Start()
    {
        IncreaseHealt(_initialHealt);
    }

    public void IncreaseHealt(int healtAmount)
    {
        _currentHealt = healtAmount + _currentHealt > _maxHealt? 
        _maxHealt : _currentHealt + healtAmount;
        _onIncreaseHealt?.Invoke();
    }

    public void ReciveDamage(int healtAmount)
    {
        _currentHealt -= healtAmount;
        if(_currentHealt < _minHealt)
        {
            _onDie?.Invoke();
        }
        _onReciveDamage?.Invoke();
        
    }
}
