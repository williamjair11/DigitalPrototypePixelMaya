using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent _enemy;
    private Vector3 _lastPlayerPosition;
    private float distance;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float walkingSpeed;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _originalEnemyPosition;
    void Start() {
        _enemy = GetComponent<NavMeshAgent>();
        _lastPlayerPosition = _playerController.savePosition();
    }
    void Update()
    {
        if (DistanceBetween(_enemy, _lastPlayerPosition) > 20f){
            _enemy.destination = _originalEnemyPosition.transform.position;
            _enemy.speed = walkingSpeed;
        }
        else{
            if (_lastPlayerPosition != _playerController.savePosition())
            {
                _enemy.destination = _lastPlayerPosition;
                _enemy.speed = runningSpeed;
            }
        }
        _lastPlayerPosition = _playerController.savePosition();
        GetEnemyState();
    }
    /// <summary>
    /// Returns the distance between two objects as a float.
    /// </summary>
    /// <param name="_navMeshAgent">The first object to compare.</param>
    /// <param name="_vector3">The second object to compare.</param>
    public float DistanceBetween(NavMeshAgent _navMeshAgent, Vector3 _vector3)
    {
        distance = Vector3.Distance(_navMeshAgent.transform.position, _vector3);
        Debug.Log(distance);
        return distance;
    }
    public enum EnemyState {
        Walking,
        Running,
        Idle,
        Turning
    }
    public EnemyState GetEnemyState() {
        if (_enemy.speed == walkingSpeed) {
            return EnemyState.Walking;
        }
        if (_enemy.speed == runningSpeed) {
            return EnemyState.Running;
        }
        if (_enemy.speed == 0f){
            return EnemyState.Idle;
        }
        else if (_enemy.velocity.magnitude > 0 && Vector3.Angle(_enemy.velocity, _enemy.transform.forward) > 10f)
        {
            return EnemyState.Turning;
        }
        else {
            return EnemyState.Idle;
        }
    }
    public float GetEnemyAngle() {
        Vector3 direction = _enemy.velocity.normalized;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        return angle;
    }
}
