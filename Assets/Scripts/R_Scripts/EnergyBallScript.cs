using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallScript : MonoBehaviour
{
    private Vector3 _lastPosition;
    [SerializeField] private float _timeToDesactivate;
    public bool _deactivating = false;
    void Update()
    {
        _lastPosition = transform.position;
    }

    public Vector3 SavePosition() 
    {
        return _lastPosition;
    }

    public IEnumerator DesactivateEnergy() 
    {
        _deactivating = true;
        yield return new WaitForSeconds(_timeToDesactivate);
        _deactivating = false;
        Destroy(this.gameObject);
    }
}
