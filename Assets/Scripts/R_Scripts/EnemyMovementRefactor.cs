using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMovementRefactor : MonoBehaviour
{
    #region Enemy Animation states
    const string ENEMY_IS_WALKING = "isWalking";
    const string ENEMY_IS_RUNNING = "isRunning";
    const string ENEMY_IS_ROTATING = "isRotating";
    const string ENEMY_IS_IDLE = "isIdle";
    const string ENEMY_IS_ATACKING = "isAtacking";
    #endregion
    #region
    AnimationController _enemyAnimatorController;
    [SerializeField] private Waypoints _waypoint;
    [HideInInspector] public NavMeshAgent _enemy;
    [SerializeField] private PlayerController _playerController;
    #endregion
    #region Enemy Parameters
    [SerializeField] private float _trackingDistance = 10f;
    [SerializeField] private float runningSpeed = 6f;
    [SerializeField] private float walkingSpeed = 3f;
    [HideInInspector] private float distanceThreshold = 1f;
    #endregion
    #region Script variables
    private Transform _currentWaypoint;
    private Vector3 _playerLastPosition;
    private Vector3 _lightSourcePosition;
    private bool _canMove = true;
    [HideInInspector] internal bool _playerIsInsideGreenLight = false;
    private bool _onLightDetection = false;
    private bool _onPlayerDetection = false;
    private bool _enemyIsPatroling = false;
    #endregion
    private void Awake() 
    {
        _playerLastPosition = _playerController.savePosition();
    }
    private void Start()
    {
        _enemyAnimatorController = GetComponent<AnimationController>();
        _enemy = GetComponent<NavMeshAgent>();

        //Set initial position to the first waypoint
        _currentWaypoint = _waypoint.GetNextWaypoint(_currentWaypoint);
        transform.position = _currentWaypoint.position;
        
        //Set the next waypoint target
        _currentWaypoint = _waypoint.GetNextWaypoint(_currentWaypoint);
    }

    private void Update()
    {
        
        _playerLastPosition = _playerController.savePosition();
        GetEnemyState();
        if (_canMove)
        {
            if (Vector3.Distance(transform.position, _playerLastPosition) < _trackingDistance)
            {
                ChasePlayer();
            }
            else
            {
                EnemyPatrols();
            }
        } 
    }
    public enum EnemyState
    {
        Walking,
        Runing,
        Rotating,
        Idle,
        Atacking,
        Turning
    }
    public EnemyState GetEnemyState()
    {
        if (_enemy.speed == walkingSpeed) {
            _enemyAnimatorController.ChangeAnimationStateTo(ENEMY_IS_WALKING);
            return EnemyState.Walking;
        }
        else if (_enemy.speed == runningSpeed){
            _enemyAnimatorController.ChangeAnimationStateTo(ENEMY_IS_RUNNING);
            return EnemyState.Runing;
        }
        else if (_enemy.speed <= 1){
            _enemyAnimatorController.ChangeAnimationStateTo(ENEMY_IS_IDLE);
            return EnemyState.Idle;
        }
        else if (_enemy.speed > 0 && Vector3.Angle(_enemy.velocity, _enemy.transform.forward) > 10f){
            _enemyAnimatorController.ChangeAnimationStateTo(ENEMY_IS_ROTATING);
            return EnemyState.Rotating;
        }
        else if (_onPlayerDetection)
        {
            _enemyAnimatorController.ChangeAnimationStateTo(ENEMY_IS_ATACKING);
            return EnemyState.Atacking;
        }
        else
        {
            _enemyAnimatorController.ChangeAnimationStateTo(ENEMY_IS_IDLE);
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
    IEnumerator Delay()
    {
        StopEnemy();
        yield return new WaitForSeconds(1f);
        _enemy.speed = walkingSpeed;
    }
        private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Light"))
        {
            _lightSourcePosition = other.gameObject.transform.position;
            _onLightDetection = true;
        }
        if (other.CompareTag("destroyOrb"))
        {
            _onLightDetection = false;
            Destroy(GameObject.Find("EnergyBall(Clone)"), 2f);
        }
        if (other.CompareTag("Player")){
            _onPlayerDetection = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Light"))
        {
            _onLightDetection = false;
        }
        else if (other.CompareTag("Player"))
        {
            _onPlayerDetection = false;
        }
    }
    void FollowLight()
    {
            _enemy.isStopped = false;
            _enemy.SetDestination(GameObject.Find("EnergyBall(Clone)").transform.position);
            _enemy.speed = runningSpeed;
            Debug.Log("Follow light");
            if (GameObject.Find("EnergyBall(Clone)") == null)return;
    }
    void EnemyPatrols()
    {
        _enemy.isStopped = false;
        Debug.Log("Enemy patrols");
        if(_onLightDetection)
        {
            FollowLight();
        }
        else
        {
            _enemy.SetDestination(_currentWaypoint.position);
            _enemy.speed = walkingSpeed;
            Debug.Log($"Distance between Enemy and waypoint{(Vector3.Distance(_enemy.transform.position, _currentWaypoint.position))}");
            if (Vector3.Distance(transform.position, _currentWaypoint.position) < distanceThreshold)
            {
                _currentWaypoint = _waypoint.GetNextWaypoint(_currentWaypoint);
                Debug.Log("next Waypoint");
            }
        }
    }
    void StopEnemy()
    {
        Debug.Log("Stop Enemy");
        _enemy.isStopped = true;
        _enemy.speed = 0f;
    }
    void ChasePlayer()
    {
        if (!_playerIsInsideGreenLight)
        {
            _enemy.isStopped = false;
            _enemy.speed = runningSpeed;
            _enemy.SetDestination(_playerLastPosition);
            Debug.Log("Chase Player");
        }
    }

}