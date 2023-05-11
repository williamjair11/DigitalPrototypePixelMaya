using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private float _originalSpeed;
    [SerializeField] private float _returnSpeed;
    [SerializeField] private NavMeshAgent _enemy;
    [SerializeField] private PlayerController _playerController = new PlayerController();
    [SerializeField] private GameObject _originalEnemyPosition;
    private Vector3 _lastPlayerPosition;
    private float distance;
    private bool _canMove= true;
    private void Start()
    {
        _lastPlayerPosition = _playerController.savePosition();
        transform.position = _originalEnemyPosition.transform.position;
    }
   
    
    void Update()
    {
        if(_canMove) 
        {
            if (DistanceBetween(_enemy, _lastPlayerPosition) > 10f)
            {
                _enemy.destination = _originalEnemyPosition.transform.position;
                _enemy.speed = _returnSpeed;
            }
            else
            {
                if (_lastPlayerPosition != _playerController.savePosition())
                {
                    _enemy.destination = _lastPlayerPosition;
                    _enemy.speed = _originalSpeed;
                }
            }
            _lastPlayerPosition = _playerController.savePosition();
        }            
    }
    public float DistanceBetween(NavMeshAgent _navMeshAgent, Vector3 _vector3)
    {
        distance = Vector3.Distance(_navMeshAgent.transform.position, _vector3);
        return distance;
    }

    public void StunEnemy(float timeStun) 
    {
        _canMove = false;
        StartCoroutine(stun(timeStun));
    }

    public IEnumerator stun (float time) 
    {
        yield return new WaitForSeconds(time);
        _canMove = true;
    }
}
