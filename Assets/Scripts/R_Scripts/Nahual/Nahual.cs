using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyType
{
    Static,
    Dynamic
}
public enum NahualStates
{
    Follow,
    Idle,
    Attack,
    Patrol
}
public enum NahualAnimations
{
    Walk,
    Idle,
    Run,
    Attack

}

public class Nahual : Shootraycast
{
    [SerializeField] private EnemyType _enemyType = EnemyType.Dynamic;
    private NahualStates _initialState = NahualStates.Idle;
#region Nahual Config
    [SerializeField] private float
    _walkSpeed = 2,
    _runSpeed = 5,
    _currentTargetDistance,
    _distanceToFollow = 10,
    _distanceToAttack = 2,
    _distanceToChangeWaypoint = 0.1f,
    _rotationFactorPerFrame = 15;
    private float _idleSpeed = 0;
#endregion
#region Ints
    [SerializeField] private int _idleAnimationsCount = 3;    
#endregion
#region Bools
    private bool _attack = false;
    private bool _follow = false;
    private bool _patrol = false;
    private bool _idle = false;
    private bool _reachWaypoint = false;
#endregion
#region References
    Animator _animator;
    [SerializeField] List<string> _targetsTags = new List<string>();
    private HealtController _healtController;
    private Vector3 _currentDestination;
    private NavMeshAgent _agent;
    [SerializeField] private Waypoints _waypoints;
    private Transform  _currentWaypoint;
#endregion
#region Getters and setters
    public NavMeshAgent Agent { get => _agent; set => _agent = value; }
    public Transform CurrentWaypoint { get => _currentWaypoint; set => _currentWaypoint = value; }
    public Animator Animator { get => _animator; set => _animator = value; }
    public Vector3 CurrentDestionation { get => _currentDestination; set => _currentDestination = value;}
    public Vector3 NewDestination { get => _targetPosition; }
    public PlayerController PlayerController { get => _playerController; }
    public TurnOnOffLight TurnOnOffLight { get => _turnOnOffLight; }
    public Waypoints Waypoints { get => _waypoints; }
    public Component ObjectType { get => _objectComponent; }
    public float WalkSpeed {get => _walkSpeed;}
    public float RunSpeed {get => _runSpeed;}
    public float CurrentTargetDistance {get => _currentTargetDistance;}
    public float DistanceToFollow {get => _distanceToFollow;}
    public float DistanceToAttack {get => _distanceToAttack;}
    public float DistanceToChangeWaypoint {get => _walkSpeed;}
    public float IdleSpeed { get => _idleSpeed; }
    public bool Atack { get => _attack; set => _attack = value; }
    public bool Idle { get => _idle; set => _idle = value; }
    public bool Patrol { get => _patrol; set => _patrol = value; }
    public bool Follow { get => _follow; set => _follow = value; }
    public bool ReachWaypoint { get => _reachWaypoint; set => _reachWaypoint = value; }
    public bool IsPlayerSafe { get => _isPlayer._safe; }
#endregion
    private void Awake()
    {
        // Create instance
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();

        // Set initial positon
        _currentWaypoint = _waypoints.GetNextWaypoint(_currentWaypoint);
        transform.position = _currentWaypoint.position;

        if (_enemyType == EnemyType.Dynamic)
        {
            _initialState = NahualStates.Patrol;
            _currentWaypoint = _waypoints.GetNextWaypoint(_currentWaypoint);
        }
        else{
            _initialState = NahualStates.Idle;
        }
    }
    private void Start()
    {
        InitializeStateMachine();
        _agent.stoppingDistance = 0;
        _agent.speed = _walkSpeed;
    }
    public override void InitializeStateMachine()
    {
        _stateFactory = new NahualStateFactory(this);
        _stateFactory.DefaultStateId = _initialState.ToString();
        _currentState = _stateFactory.GetState(_initialState.ToString());
        _currentState.OnStartState();
    }
    private void Update()
    {
        _agent.SetDestination(_currentDestination);
        if(ShootRaycast(_targetsTags))
        {
            _follow = true;
        }
        else
        {
            _follow = false;
        }
        _currentState.Update();
    }
    void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = transform.position.x;
        positionToLookAt.y = 0f;
        positionToLookAt.z = transform.position.z;
        Quaternion currentRotation = transform.rotation;

        if(_patrol || _follow)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, _rotationFactorPerFrame * Time.deltaTime);
        }
    }
    
}
