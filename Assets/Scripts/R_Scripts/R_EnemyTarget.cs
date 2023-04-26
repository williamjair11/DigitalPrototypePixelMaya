using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// This class controls the enemy's movement towards the player and return to the original position.
/// </summary>
public class R_EnemyTarget : MonoBehaviour
{
    [SerializeField] private float _originalSpeed;
    [SerializeField] private float _returnSpeed;
    [SerializeField] private NavMeshAgent _enemy;
    [SerializeField] private GameObject _player;
    // private PlayerController _playerController = new PlayerController();
    [SerializeField] private GameObject _originalEnemyPosition;
    private Vector3 _lastPlayerPosition = Vector3.zero;
    private float distance;
    private void Start() {
        // _lastPlayerPosition = _playerController.savePosition();
        _lastPlayerPosition = _player.transform.position;
        _enemy.destination = _originalEnemyPosition.transform.position;
    }
   
    
    void Update()
    {
        if (DistanceBetween(_enemy, _lastPlayerPosition) > 20f)
        {
            _enemy.destination = _originalEnemyPosition.transform.position;
            _enemy.speed = _returnSpeed;
        }
        else
        {
            // if (_lastPlayerPosition != _playerController.savePosition())
            if (_lastPlayerPosition != _player.transform.position)
            {
                //_enemy.destination = _lastPlayerPosition;
                _enemy.speed = _originalSpeed;
                _enemy.destination = _player.transform.position;
                _lastPlayerPosition = _player.transform.position;
            }
        }
        Debug.Log(distance);
    }
    /// <summary>
    /// Returns the distance between two objects as a float.
    /// </summary>
    /// <param name="_navMeshAgent">The first object to compare.</param>
    /// <param name="_vector3">The second object to compare.</param>
    private float DistanceBetween(NavMeshAgent _navMeshAgent, Vector3 _vector3)
    {
        distance = Vector3.Distance(_navMeshAgent.transform.position, _vector3);
        return distance;
    }
}
