using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    #region Enemy Animation states
    const string ENEMY_IS_WALKING = "isWalking";
    const string ENEMY_IS_RUNNING = "isRunning";
    const string ENEMY_IS_ROTATING = "isRotating";
    const string ENEMY_IS_IDLE = "isIdle";
    const string ENEMY_IS_ATTACKING = "isAtacking";
    #endregion
    #region Enemy configuration
    NavMeshAgent _enemy;
    [SerializeField] private float _walkingSpeed;
    [SerializeField] private float runningSpeed;
    private float _stoppedSpeed = 0f;
    [SerializeField] 
    private float _trackingDistance;
    [SerializeField] [Tooltip("Distance threshold between enemy and waypoint")]
    private float _distanceThreshold = 1f;
    private float _rotatingMagnitud = 10f;
    #endregion
    #region Name
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Waypoints _waypoints;
    AnimationController _enemyAnimatorController;
    #endregion
    #region Name
    Transform _currentWaypoint;
    private bool _canMove = true;
    private Vector3 _playerLastPosition;
    [HideInInspector] internal bool _playerIsInsideGreenLight;
    private float _distanceForPlayerBeingHitByEnemy = 1.5f;
    private bool _isTheEnemyHittingPlayer = false;
    private RaycastHit hit;
    private float _stopDistance = 1.5f;
    private bool _onPlayerDetection = false;
    float raycastDistance = 10f;
    float raycastRadius = 6f;


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
        _playerLastPosition = _playerController.savePosition();
        SetEnemyAnimation();
        if (_canMove)
        {
            if ((Vector3.Distance(transform.position, _playerLastPosition) < _trackingDistance) && !_playerIsInsideGreenLight)
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
        //Recuerden quitar el autobrake del NavMeshAgent component
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
        float verticalOffset = 1;
        Vector3 raycastOrigin = transform.position + Vector3.up * verticalOffset;
        if (Physics.Raycast(transform.position, _playerLastPosition - transform.position, out hit, _trackingDistance))
        {
            if (hit.collider.CompareTag("PlayerDetection"))
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
        if (other.CompareTag("PlayerDetection"))
        {
            _isTheEnemyHittingPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("PlayerDetection"))
        {
            _isTheEnemyHittingPlayer = false;
        }
        
    }
    void ShootRaycast()
    {
        float raycastRadius = 2f;
        float raycastDistance = 10f;
        float verticalOffset = 1f;

        // Calculate the modified origin position
        Vector3 raycastOrigin = transform.position + Vector3.up * verticalOffset;

        // Perform the sphere cast
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, raycastRadius, transform.forward, raycastDistance);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("PlayerDetection"))
            {
                _onPlayerDetection = true;
            }
            else{
                _onPlayerDetection = false;
            }
        }
        Debug.DrawRay(raycastOrigin, transform.forward * raycastDistance, Color.red); // Draw a line representing the direction and distance of the sphere cast
    }
    private void OnDrawGizmosSelected() {
        float verticalOffset = 1;
        Vector3 raycastOrigin = transform.position + Vector3.up * verticalOffset;
        Gizmos.color = Color.red;
        Debug.DrawLine(raycastOrigin, transform.position + transform.forward * raycastDistance);
        Gizmos.DrawWireSphere(raycastOrigin + transform.forward * raycastDistance,raycastRadius);
    }
}

//((Vector3.Distance(transform.position, _playerLastPosition) < _trackingDistance) && !_playerIsInsideGreenLight)
