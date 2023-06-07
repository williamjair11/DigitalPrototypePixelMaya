using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetection : MonoBehaviour
{
    OrbPosition _orbPosition;
    [SerializeField] private float _rangeOfLightDetection;
    private bool _isEnemyDetectingLight;
    private bool _isEnemyDetectingWhiteTorch;
    private Ray _ray = new Ray();
    [HideInInspector] public bool _enemyIsOnGreenLight;
    EnemyMovement _enemyMovement;

    void Start()
    {
        GetEnemyRay();
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    void Update()
    {
        EnemyIsDetectingLight();
    }
    private void GetEnemyRay()
    {
        Vector3 direction = Vector3.forward;
        _ray = new Ray(transform.position, transform.TransformDirection(direction * _rangeOfLightDetection));
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * _rangeOfLightDetection));

    }
    public bool EnemyIsDetectingWhiteTorch()
    {
        if (Physics.Raycast(_ray, out RaycastHit hit, _rangeOfLightDetection))
        {
            if (hit.collider.tag == "whiteTorch")
            {
                _isEnemyDetectingWhiteTorch = true;
            }
            else
            {
                _isEnemyDetectingWhiteTorch = false;
            }
            return _isEnemyDetectingWhiteTorch;
        }
        return _isEnemyDetectingWhiteTorch;
    }
    public bool EnemyIsDetectingLight()
    {
        if (Physics.Raycast(_ray, out RaycastHit hit, _rangeOfLightDetection))
        {
            if (hit.collider.tag == "playerLightOrb")
            {
                _isEnemyDetectingLight = true;
            }
            else
            {
                _isEnemyDetectingLight = false;
            }
        }
        return _isEnemyDetectingLight;
    }
    public enum EnemyIsTracking
    {
        Orb,
        LightTorch,
        Nothing
    }
    public EnemyIsTracking OnLightDetection()
    {
        if (EnemyIsDetectingLight())
        {
            return EnemyIsTracking.Orb;
        }
        else if (!EnemyIsDetectingLight())
        {
            return EnemyIsTracking.Nothing;
        }
        else if (EnemyIsDetectingWhiteTorch())
        {
            return EnemyIsTracking.LightTorch;
        }
        else
        {
            return EnemyIsTracking.Nothing;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("greenTorch"))
        {
            _enemyMovement._enemy.destination = _enemyMovement._currentWaypoint.position;
        }
        if (other.CompareTag("playerLightOrb") && other.Raycast(_ray, out RaycastHit hit, _rangeOfLightDetection))
        {
            _enemyMovement._enemy.destination = other.transform.position;
        }
        if (other.CompareTag("whiteTorch") && other.Raycast(_ray, out hit, _rangeOfLightDetection))
        {
            _enemyMovement._enemy.destination = other.transform.position;
        }
    }
}
