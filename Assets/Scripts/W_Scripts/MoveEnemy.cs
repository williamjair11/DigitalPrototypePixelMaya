using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class MoveEnemy : MonoBehaviour
{
    private int _indexWaypoints;
    private Vector3 _target;
    private Vector3 _lastTarget;
    private AnimationController animationController;
      
    enum EnemyState {Patrol, ChasePlayer, ChaseWhitelight, Stun, Attacking , Observing }
    private EnemyState _enemyState;

    enum TypeEnemy {Dinamic, Observing}
    [SerializeField] private TypeEnemy _typeEnemy;
    NavMeshAgent _agent;


    [SerializeField] private float _speedEnemyWalking;
    [SerializeField] private float _speedEnemyRunning;
    [SerializeField] private float _rangeToChasePlayer;
    [SerializeField] private Transform _initialWaypoint;
    [SerializeField] private Transform[] _waypoints;


    private bool _isChasePlayer;
    private bool _inPosition = false;
    private GreenZone _greenZone;


    private PlayerController _playerController;
    private NormalEnergyBall _normalEnergyBall;
    void Start()
    {

        _agent = GetComponent<NavMeshAgent>();
        _playerController= FindObjectOfType<PlayerController>();
        animationController = GetComponent<AnimationController>();
        _greenZone = FindObjectOfType<GreenZone>();

        if (_typeEnemy == TypeEnemy.Dinamic)
        {
            UpdateDestination();
        }
           
        if(_typeEnemy  == TypeEnemy.Observing) 
        { 
            ReturnToPositionObserving();
        }
    }
    void Update()
    {

        switch (_enemyState)
        {
            case EnemyState.Patrol:
                _agent.speed = _speedEnemyWalking;               
                SavedDistanceWaypoints();
                break;
            case EnemyState.ChasePlayer:
                ChasePlayer();
                break;
            case EnemyState.ChaseWhitelight:
                _agent.speed = _speedEnemyRunning;
                ChaseWhitelight();
                break;
            case EnemyState.Attacking:

                break;
            case EnemyState.Stun:

                break;
            case EnemyState.Observing:
                if (_inPosition == false)
                {
                    ReturnToPositionObserving();
                }
                break;
        }       
    }

    void SavedDistanceWaypoints() 
    {
        if (Vector3.Distance(transform.position, _target) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }
    void UpdateDestination() 
    {
            _target = _waypoints[_indexWaypoints].position;
            _agent.SetDestination(_target);
            _lastTarget = _target;
            //Debug.Log("patrullando");
    }

    void IterateWaypointIndex() 
    {
        _indexWaypoints++;
        if(_indexWaypoints == _waypoints.Length) 
        {
            _indexWaypoints = 0;
        }
    }

    void ChasePlayer() 
    {
        Vector3 _playerPosition = _playerController.savePosition();
        _isChasePlayer = true;
        _agent.speed = _speedEnemyRunning;

        if (_isChasePlayer &&  Vector3.Distance(transform.position, _playerPosition) < _rangeToChasePlayer)
        {
            _agent.SetDestination(_playerPosition);            
        }
        else 
        {
            if (_typeEnemy == TypeEnemy.Dinamic)
            {
                _enemyState = EnemyState.Patrol;
                _isChasePlayer = false;
                _agent.SetDestination(_lastTarget);
            }
            else 
            {
                _enemyState = EnemyState.Observing;
                _inPosition = false;
            }
        }
    }

    void ChaseWhitelight() 
    {
        bool deactiving = false;
        
        if (!deactiving) { _agent.SetDestination(_normalEnergyBall.SavePosition()); }

        if (Vector3.Distance(transform.position, _normalEnergyBall.SavePosition()) < 1 && _normalEnergyBall._deactivating == false) 
        {
            deactiving = true;
            _agent.isStopped = true;
            StartCoroutine(_normalEnergyBall.DesactivateEnergy());
        }

        if (_normalEnergyBall == null)       
        {
            switch (_typeEnemy) 
            {
                case TypeEnemy.Dinamic:
                    _enemyState = EnemyState.Patrol;
                    _agent.isStopped = false;
                    _agent.SetDestination(_lastTarget);
                    
                    break;
                case TypeEnemy.Observing:
                    _enemyState = EnemyState.Observing;
                    _agent.isStopped = false;
                    _inPosition = false;
                    break;
            }                        
        }
    }

    public void StunEnemy(float timeStun) 
    {
        StartCoroutine(TimeStun(timeStun));
    }

    IEnumerator TimeStun(float timeStun)
    {
        Debug.Log("estuneado");
        _enemyState = EnemyState.Stun;
        _agent.isStopped = true;
        yield return new WaitForSeconds(timeStun);
        _agent.isStopped = false;
        _isChasePlayer = false;

        if (_typeEnemy == TypeEnemy.Dinamic)
        {
            _enemyState = EnemyState.Patrol;
            _agent.SetDestination(_lastTarget);
        }
        else 
        {
            ReturnToPositionObserving();
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {            
           _enemyState = EnemyState.ChasePlayer;                       
        }

        if (other.tag == "WhiteLight")
        {
            if (!_isChasePlayer)
            {
                _enemyState = EnemyState.ChaseWhitelight;
                _normalEnergyBall = other.GetComponent<NormalEnergyBall>();
            }
        }

        if (other.tag == "WhiteTorch")
        {
            if (!_isChasePlayer)
            {
                
            }
        }
    }
    public void ReturnToPositionObserving() 
    {
        //Debug.Log("regresando");
        _agent.SetDestination(_initialWaypoint.position);       
        _inPosition = true;
    }
}
