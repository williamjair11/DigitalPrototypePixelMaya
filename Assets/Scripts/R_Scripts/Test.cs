using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    #region Enemy Animation states
    const string ENEMY_IS_WALKING = "isWalking";
    const string ENEMY_IS_RUNNING = "isRunning";
    const string ENEMY_IS_ROTATING = "isRotating";
    const string ENEMY_IS_IDLE = "isIdle";
    const string ENEMY_IS_ATTACKING = "isAtacking";
    #endregion
    #region Enemy movement settings
    NavMeshAgent _enemy;
    [SerializeField] private float _walkingSpeed;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float _trackingDistance;

    [SerializeField] [Tooltip("Distance threshold between enemy and waypoint")]
    private float _distanceThreshold = 1f;
    private float _rotatingMagnitud = 10f;
    private float _stoppedSpeed = 0f;
    #endregion
    #region Name
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Waypoints _waypoints;
    AnimationController _enemyAnimatorController;
    #endregion
    #region bools
    Transform _currentWaypoint;
    private bool _canMove = true;
    private Vector3 _playerLastPosition;
    [HideInInspector] internal bool _playerIsInsideGreenLight;
    private bool _isTheEnemyHittingPlayer = false;
    private float _stopDistance = 1.5f;
    private bool _onPlayerDetection = false;
    #endregion
    #region Raycast
    [Header("Raycast settings")]
    private RaycastHit hit;
    [SerializeField]
    float raycastRadius = 5f;
    [SerializeField]
    float raycastDistance = 15f;
    [SerializeField]
    float verticalOffset = 1f;

    [SerializeField] [Tooltip("The distance that the enemy will start playing the attack animation")]
    private float _distanceForPlayerBeingHitByEnemy = 1.5f;

    [SerializeField] [Tooltip("The tag name of the object that will be chased by the enemy")]
     private string _playerTag;
    #endregion
    private void Awake() 
    {
        _enemyAnimatorController = GetComponent<AnimationController>();
        _enemy = GetComponent<NavMeshAgent>();
        _playerLastPosition = _playerController.savePosition();
    }
    private void Start() 
    {
        //Set initial position to the first waypoint
        _currentWaypoint = _waypoints.GetNextWaypoint(_currentWaypoint);
        transform.position = _currentWaypoint.position;
        
        //Set the next waypoint target
        _currentWaypoint = _waypoints.GetNextWaypoint(_currentWaypoint); 
    }
    private void Update() 
    {
        Debug.Log($"VELOCITY{_enemy.velocity.magnitude}");
        Debug.Log($"SPEED{_enemy.speed}");
        Debug.Log($"isStopped{_enemy.isStopped}");
        _playerLastPosition = _playerController.savePosition();
        ShootRaycast();
        SetEnemyAnimation();
        if (_canMove)
        {
            if (_onPlayerDetection && !_playerIsInsideGreenLight)
            {
                ChasePlayer();
            }
            else
            {
                PatrolArea();
            }
        }
    }
    void ChasePlayer()
    {
        Vector3 raycastOrigin = transform.position + Vector3.up * verticalOffset;

        Vector3 direction = (_playerLastPosition - transform.position).normalized;
        Vector3 targetPosition = _playerLastPosition - direction * _stopDistance;

        if (Vector3.Distance(transform.position, targetPosition) < _stopDistance)
        {
        _enemy.speed = _stoppedSpeed;
        }
        else
        {
            _enemy.speed = runningSpeed;
            _enemy.SetDestination(_playerLastPosition);
        }
        if (Physics.Raycast(raycastOrigin, transform.forward, out hit, _distanceForPlayerBeingHitByEnemy))
        {
            if (hit.collider.CompareTag(_playerTag))
            {
                _isTheEnemyHittingPlayer = true;
            }
            else{
                _isTheEnemyHittingPlayer = false;
            }
        }
        Debug.Log("Chase Player");
        
    }
    void PatrolArea()
    {
        _enemy.speed = _walkingSpeed;
        if (_enemy.remainingDistance < _distanceThreshold)
        {
            _currentWaypoint = _waypoints.GetNextWaypoint(_currentWaypoint);
            _enemy.SetDestination(_currentWaypoint.position);
        }else{
                _enemy.SetDestination(_currentWaypoint.position);
        }
        Debug.Log("Patrol area");
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
    private void SetEnemyAnimation()
    {
        if (_enemy.speed == _walkingSpeed) {
            _enemyAnimatorController.ChangeAnimationStateTo(ENEMY_IS_WALKING);
            Debug.Log("1");
        }
        else if (_enemy.speed == runningSpeed){
            _enemyAnimatorController.ChangeAnimationStateTo(ENEMY_IS_RUNNING);
            Debug.Log("2");
        }
        else if (_enemy.speed == _stoppedSpeed && !_isTheEnemyHittingPlayer){
            _enemyAnimatorController.ChangeAnimationStateTo(ENEMY_IS_IDLE);
            Debug.Log("3");
        }
        else if (_enemy.speed > 0 && Vector3.Angle(_enemy.velocity, _enemy.transform.forward) > 10f){
            _enemyAnimatorController.ChangeAnimationStateTo(ENEMY_IS_ROTATING);
            Debug.Log("4");
        }
        else if (_enemy.speed == _stoppedSpeed && _isTheEnemyHittingPlayer)
        {
            _enemyAnimatorController.ChangeAnimationStateTo(ENEMY_IS_ATTACKING);
            Debug.Log("5");
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(_playerTag))
        {
            _isTheEnemyHittingPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag(_playerTag))
        {
            _isTheEnemyHittingPlayer = false;
        }
        
    }
    void ShootRaycast()
    {
        Vector3 raycastOrigin = transform.position + Vector3.up * verticalOffset;
        if (Physics.SphereCast(raycastOrigin, raycastRadius, transform.forward, out hit, _trackingDistance))
        {
            if (hit.collider.CompareTag(_playerTag))
            {
                _onPlayerDetection = true; 
            }
            else{
                _onPlayerDetection = false;
            }
        }
        // Debug.DrawRay(raycastOrigin, transform.forward * raycastDistance, Color.red);
    }
    private void OnDrawGizmosSelected() {
        Vector3 raycastOrigin = transform.position + Vector3.up * verticalOffset;
        Gizmos.color = Color.red;
        Debug.DrawLine(raycastOrigin, transform.position + transform.forward * _trackingDistance);
        Gizmos.DrawWireSphere(raycastOrigin + transform.forward * _trackingDistance,raycastRadius);
    }
}