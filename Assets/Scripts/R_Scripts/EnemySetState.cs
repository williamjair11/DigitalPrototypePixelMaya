using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySetState : MonoBehaviour
{
     #region NavMeshAgent Animation states
    const string ENEMY_IS_WALKING = "isWalking";
    const string ENEMY_IS_RUNNING = "isRunning";
    const string ENEMY_IS_ROTATING = "isRotating";
    const string ENEMY_IS_IDLE = "isIdle";
    const string ENEMY_IS_ATTACKING = "isAtacking";
    #endregion
    #region NavMeshAgent movement settings
    [SerializeField] private float _walkingSpeed;
    [SerializeField] private float _runningSpeed;
    [SerializeField] private float _trackingDistance;

    [SerializeField] [Tooltip("Distance threshold between enemy and waypoint")]
    private float _distanceThreshold = 1f;
    private float _rotatingMagnitud = 10f;
    private float _stoppedSpeed = 0f;
    float _stopDistance = 0f;
    #endregion
    NavMeshAgentStates _navMeshAgentStates;
    NavMeshView _navMeshView;
    Vector3 _gameObjectPosition;
    [SerializeField] private PlayerController _playerController;
    Shootraycast _shootRayCast;
    PlayerSafePoint _playerSafePoint;
    NavMeshAgent _navMeshAgent;
    NavMeshAnimations _navMeshAnimations;
    #region Waypoints
    [SerializeField] private Waypoints _waypoints;
    Transform _currentWaypoints;
    #endregion
    #region tags
    [SerializeField] private string _whiteOrbtag;
    [SerializeField] private string _whiteTorchTag;
    [SerializeField] private string _playerTag;
    #endregion



    private void Awake() {
        _navMeshAnimations = GetComponent<NavMeshAnimations>();
        _navMeshAgentStates = GetComponent<NavMeshAgentStates>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _shootRayCast = GetComponent<Shootraycast>();
    }
    private void Start() {
        _currentWaypoints = _waypoints.GetNextWaypoint(_currentWaypoints);
        transform.position = _currentWaypoints.position;

        _currentWaypoints = _waypoints.GetNextWaypoint(_currentWaypoints);
        _navMeshView = new NavMeshView();
    }
    private void Update() {
        _gameObjectPosition = _playerController.savePosition();

        if (_shootRayCast.ShootRaycast(_whiteOrbtag))
        {
            ICommand _followWhiteLight = new FollowWhitwLightCommand(_navMeshAgentStates, _navMeshAgent, _shootRayCast.GetWhiteLightPosition(_whiteOrbtag), _runningSpeed, _stopDistance);
            _navMeshView.AddStateCommand(_followWhiteLight);

            // ICommandAnimations _run = new EnemySetAnimations(_navMeshAnimations, ENEMY_IS_RUNNING);
            // _navMeshView.AddAnimationCommand(_run);
        }
        else if (_shootRayCast.ShootRaycast(_whiteTorchTag))
        {
            ICommand _followWhiteTorch = new FollowWhitwLightCommand(_navMeshAgentStates, _navMeshAgent, _shootRayCast.GetWhiteLightPosition(_whiteTorchTag), _runningSpeed, _stopDistance);
            _navMeshView.AddStateCommand(_followWhiteTorch);

            // ICommandAnimations _run = new EnemySetAnimations(_navMeshAnimations, ENEMY_IS_RUNNING);
            // _navMeshView.AddAnimationCommand(_run);
        }
        else if (_shootRayCast.ShootRaycast(_playerTag) && _playerSafePoint.IsGameObjectInsideGreenTorch())
        {
            ICommand _chasePlayer = new ChaseGameObjectCommand(_navMeshAgentStates, _navMeshAgent, _gameObjectPosition, _runningSpeed, _stopDistance);
            _navMeshView.AddStateCommand(_chasePlayer);

            // ICommandAnimations _run = new EnemySetAnimations(_navMeshAnimations, ENEMY_IS_RUNNING);
            // _navMeshView.AddAnimationCommand(_run);
        }
        else
        {
            ICommand _patrolArea = new PatrolCommand(_navMeshAgentStates, _navMeshAgent, _currentWaypoints, _walkingSpeed, _stopDistance);
            _navMeshView.AddStateCommand(_patrolArea);

            // ICommandAnimations _walking = new EnemySetAnimations(_navMeshAnimations, ENEMY_IS_WALKING);
            // _navMeshView.AddAnimationCommand(_walking);
        }

    }
}
