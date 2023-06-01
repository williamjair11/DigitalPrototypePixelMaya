using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    EnemyAnimationController _enemyAnimatorController;
    [SerializeField] private Waypoints _waypoint;
    [HideInInspector] public Transform _currentWaypoint;
    public NavMeshAgent _enemy;
    private Vector3 _lastPlayerPosition;
    private float distance;
    private bool _playerMoved;
    [SerializeField] private float tracksPlayerWhitinRangeOf;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float walkingSpeed;
    [SerializeField] private PlayerController _playerController;
    // public GameObject _enemyCheckPoint1;
    // public GameObject _enemyCheckPoint2;
    LightDetection _lightDetection;
    private bool _canMove = true;
    // [SerializeField] private List<GameObject> checkPointList = new List<GameObject>();
    // [SerializeField] private GameObject checkPointSpawnPosition;
    [SerializeField] private float distanceThreshold = 0.1f;

    void Start()
    {
        _enemyAnimatorController = GetComponent<EnemyAnimationController>();
        _lightDetection = GetComponent<LightDetection>();
        _enemy = GetComponent<NavMeshAgent>();
        _lastPlayerPosition = _playerController.savePosition();
        //Set initial position to the first waypoint
        _currentWaypoint = _waypoint.GetNextWaypoint(_currentWaypoint);
        transform.position = _currentWaypoint.position;

        //Set the newx waypoint target
        _currentWaypoint = _waypoint.GetNextWaypoint(_currentWaypoint);
    }
    void Update()
    {
        if (_canMove)
        {
            if (InitialPlayerPositionChange())
            {
                if (DistanceBetween(_enemy, _lastPlayerPosition) > tracksPlayerWhitinRangeOf)
                {
                    _enemy.speed = walkingSpeed;
                    _enemy.destination = _currentWaypoint.position;
                    if (Vector3.Distance(transform.position, _currentWaypoint.position) < distanceThreshold)
                    {
                        _currentWaypoint = _waypoint.GetNextWaypoint(_currentWaypoint);
                        _enemy.destination = _currentWaypoint.position;
                    }
                }
                else
                {
                    if (DistanceBetween(_enemy, _lastPlayerPosition) < 1f)
                    {
                        _enemy.speed = 0f;
                    }
                    else
                    {
                        _enemy.destination = _lastPlayerPosition;
                        _enemy.speed = runningSpeed;
                    }

                }
            }
            _lastPlayerPosition = _playerController.savePosition();
            GetEnemyState();
        }
        else 
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

    }
    /// <summary>
    /// Returns the distance between two objects as a float.
    /// </summary>
    /// <param name="_navMeshAgent">The first object to compare.</param>
    /// <param name="_vector3">The second object to compare.</param>
    public float DistanceBetween(NavMeshAgent _navMeshAgent, Vector3 _vector3)
    {
        distance = Vector3.Distance(_navMeshAgent.transform.position, _vector3);
        //Debug.Log(distance);
        return distance;
    }
    public enum EnemyState
    {
        Walking,
        Running,
        Idle,
        Turning
    }
    public EnemyState GetEnemyState()
    {
        if (_enemy.speed == walkingSpeed)
        {
            return EnemyState.Walking;
        }
        else if (_enemy.speed == runningSpeed)
        {
            return EnemyState.Running;
        }
        else if (_enemy.speed < 1f)
        {
            return EnemyState.Idle;
        }
        else if (_enemy.velocity.magnitude > 0 && Vector3.Angle(_enemy.velocity, _enemy.transform.forward) > 10f)
        {
            return EnemyState.Turning;
        }
        else if (DistanceBetween(_enemy, _lastPlayerPosition) < 3f)
        {
            return EnemyState.Idle;
        }
        else
        {
            return EnemyState.Idle;
        }
    }
    public void StunEnemy(float timeStun)
    {
        _canMove = false;
        StartCoroutine(stun(timeStun));
    }

    public IEnumerator stun(float time)
    {
        yield return new WaitForSeconds(time);
        _canMove = true;
    }
    private bool InitialPlayerPositionChange()
    {
        if (_lastPlayerPosition != _playerController.savePosition())
        {
            _playerMoved = true;
        }
        else
        {
            _playerMoved = false;
            if (GetEnemyState() == EnemyState.Idle)
            {
                _enemyAnimatorController.enemyaAnimator.SetBool(_enemyAnimatorController.isIdleHash, true);
            }
        }
        return _playerMoved;
    }
    // private void CreateCheckpoints()
    // {
    //     foreach (GameObject obj in checkPointList)
    //     {
    //         Instantiate(obj);
    //     }
    // }

}