using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This class controls the enemy's movement towards the player and return to the original position.
/// </summary>
public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private float _originalSpeed;
    [SerializeField] private float _returnSpeed;
    [SerializeField] private NavMeshAgent _enemy;
    [SerializeField] private PlayerController _playerController = new PlayerController();
    [SerializeField] private GameObject _originalEnemyPosition;
    private Vector3 _lastPlayerPosition;
    private float distance;
    private void Start() {
        _lastPlayerPosition = _playerController.savePosition();
    }
   
    
    void Update()
    {
        if (DistanceBetween(_enemy, _lastPlayerPosition) > 20f){
            _enemy.destination = _originalEnemyPosition.transform.position;
            _enemy.speed = _returnSpeed;
        }
        else{
            if (_lastPlayerPosition != _playerController.savePosition())
        {
            _enemy.destination = _lastPlayerPosition;
            _enemy.speed = _originalSpeed;
        }
        }
        Debug.Log(distance);
        _lastPlayerPosition = _playerController.savePosition();
        
    }
    /// <summary>
    /// Returns the distance between two objects as a float.
    /// </summary>
    /// <param name="_navMeshAgent">The first object to compare.</param>
    /// <param name="_vector3">The second object to compare.</param>
    public float DistanceBetween(NavMeshAgent _navMeshAgent, Vector3 _vector3)
    {
        distance = Vector3.Distance(_navMeshAgent.transform.position, _vector3);
        return distance;
    }
}
