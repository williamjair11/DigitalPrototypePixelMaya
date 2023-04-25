using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnergyBallController : MonoBehaviour
{
    private bool _shotAvailable = true;

    [SerializeField]
    private Transform _initialPositionBall;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private GameObject _EnergyBallPrefab;

    [SerializeField]
    private float _ballEnergyCost;

    [SerializeField]
    private float _timeNextShot;

    [SerializeField]
    private float _ballForceForward;

    [SerializeField]
    private float _ballForceUp;

    [SerializeField]
    private UnityEvent<float> _setEnergyEvent;

    public void throwButton()
    {
        float _temporalCurrentEnergy = GetComponent<EnergyController>()._currentEnergy;
        
        if (_shotAvailable && _temporalCurrentEnergy>_ballEnergyCost)
        {
            GameObject _temporaryEnergyBall = Instantiate(_EnergyBallPrefab, _initialPositionBall.transform.position, _initialPositionBall.transform.rotation);
            Rigidbody _rb = _temporaryEnergyBall.GetComponent<Rigidbody>();
            _rb.AddForce(_temporaryEnergyBall.transform.forward * _ballForceForward, ForceMode.Impulse);
            _rb.AddForce(_temporaryEnergyBall.transform.up * _ballForceUp, ForceMode.Impulse);
            _setEnergyEvent.Invoke(_ballEnergyCost);
            _shotAvailable = false;
            StartCoroutine(waitForNextShot());
            Destroy(_temporaryEnergyBall, 5f);
        }
        else 
        {
            
        }
    }
    IEnumerator waitForNextShot()
    {
        yield return new WaitForSeconds(_timeNextShot);
        _shotAvailable = true;
    }
}
