using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTargetDetector : MonoBehaviour
{
    EnemyMovement _enemyMovement;
    NavMeshAgent _enemy;
    PlayerController _player;

    [SerializeField] private float _trackingDistance;
    [SerializeField] private Animation _enemyAnimationIfPlayerIsDetected;
    /// <summary>
    /// Returns a bool if the NavMeshAgent is close to the player using the variable _trackingDisttance to set a distance between the enemy and the player
    /// </summary>
    bool IsClose()
    {
        if (_enemyMovement.DistanceBetween(_enemy, _player.transform.position) < _trackingDistance)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    void Start()
    {
        _enemyMovement = GetComponent<EnemyMovement>();  
        _enemy = GetComponent<NavMeshAgent>();
        _player = GetComponent<PlayerController>();



    }
    void Update()
    {
        
    }
}
