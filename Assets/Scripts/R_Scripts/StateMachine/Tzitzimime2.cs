using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyType
{
    Static,
    Dynamic
    
}
public enum TzitzimimeStates
{
    Walking,
    Following,
    Idle,
    Attacking
}
public enum TzitzimimeAnimations
{
    Walking,
    Following,
    Idle,
    Greeting,
    Running,
    Attacking

} 
[RequireComponent(typeof(Waypoints), typeof(NavMeshAgent), typeof (HealtController))]
public class Tzitzimime2 : StateMachineCtx
{
    [SerializeField] private EnemyType _enemyType = EnemyType.Dynamic;
    [SerializeField] private TzitzimimeStatesId _initialState = TzitzimimeStatesId.Idle;    
#region Floats
    [SerializeField] private float
    _walkSpeed = 2,
    _runSpeed = 5,
    _currentTargetDistance,
    _distanceToLook = 30,
    _distanceToGreet = 25,
    _distanceToWalk = 20,
    _distanceToFollow = 10,
    _distanceToAttack = 2;
#endregion
#region Ints
    [SerializeField] private int _idleAnimationsCount = 3;    
#endregion
#region Getters and setters
    public Animator Animator { get => _animator; }
    public GameObject Target { get => _currentTarget; }
    public NavMeshAgent Agent{get=>_agent;}
    public int IdleAnimationsCount { get => _idleAnimationsCount; }
    public float RunSpeed { get => _runSpeed; }
    public float WalkSpeed { get => _walkSpeed; }
    public float CurrentTargetDistance { get => _currentTargetDistance; }
    public float DistanceToLook { get => _distanceToLook; }
    public float DistanceToGreet { get => _distanceToGreet; }
    public float DistanceToWalk { get => _distanceToWalk; }
    public float DistanceToFollow { get => _distanceToFollow; }
    public float DistanceToAttack { get => _distanceToAttack; }
    public bool Greeting { get => _greeting; set => _greeting = value; }
    public bool Walking { get => _walking; set => _walking = value; }
    public bool Following { get => _following; set => _following = value; }
    public bool Looking { get => _looking; set => _looking = value; }
    public bool Attacking { get => _attacking; set => _attacking = value; }
#endregion
#region References
    Animator _animator;
    // [SerializeField] private GameObject _target;
    [SerializeField] List<string> _targetsTags = new List<string>();
    Shootraycast _shootRayCast;
    private HealtController _healtController = null;
    private GameObject _currentTarget = null;
    private NavMeshAgent _agent = null;
    [SerializeField] private Waypoints _waypoints = null;
    private Transform  _currentWaypoint = null;
#endregion
#region Bools
    private bool _greeting = false;
    private bool _walking = false;
    private bool _following = false;
    private bool _looking = false;
    private bool _attacking = false;
#endregion
    void Awake()
    {
        //Get reference
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _waypoints = GetComponent<Waypoints>();
        
        // Set enemy waypoints
    }
    void Start()
    {
        InitializeStateMachine();
        _agent.stoppingDistance = 0;
        _agent.speed = _walkSpeed;
    }
    public override void InitializeStateMachine()
    {
        //Guarda en una variable la instancia de StateFactory
        _stateFactory = new TzitzimimeSF(this);
        //guarda el initial state como deafult state del state machine (estado 
        //que siempre tomara en caso de un error)
        _stateFactory.DefaultStateId = _initialState.ToString();
        //Iniciamos el primer stado.
        _currentState = _stateFactory.GetState(_initialState.ToString());
        _currentState.OnStartState();
    }
    void Update()
    {
        CheckTargetDistance();
        //Es importante colocar _currentSate.Update al final de todas las lineas de código
        _currentState.Update();
    }
    void CheckTargetDistance()
    {
        if (_currentTarget == null) {
            _currentTarget = _currentWaypoint.gameObject;
        }
        _currentTargetDistance =
        Vector3.Distance(_currentTarget.transform.position, transform.position);
        if (_currentTargetDistance > _distanceToWalk)
        {
            _looking = true;
            _agent.isStopped = true;
            transform.LookAt(_currentTarget.transform.position);
        }
        else _looking = false;

        if (_currentTargetDistance <= DistanceToGreet && _currentTargetDistance > _distanceToWalk)
        {
            _greeting = true;
            _agent.isStopped = true;
            transform.LookAt(_currentTarget.transform.position);
        }
        else _greeting = false;

        if (_currentTargetDistance <= _distanceToWalk && _currentTargetDistance > DistanceToFollow)
        {
            _agent.SetDestination(_currentTarget.transform.position);
            _walking = true;
            _agent.isStopped = false;
        }
        else _walking = false;

        if (_currentTargetDistance <= DistanceToFollow && _currentTargetDistance > _distanceToAttack)
        {
            _agent.SetDestination(_currentTarget.transform.position);
            _agent.isStopped = false;
            _following = true;
        }
        else _following = false;

        if (_currentTargetDistance <= DistanceToAttack)
        {
            _attacking = true;
            _agent.isStopped = true;
        }
        else _attacking = false;
    }
    void GetWaypoint()
    {
        if (_waypoints != null)
        {
            _currentWaypoint = _waypoints.GetNextWaypoint(_currentWaypoint);
            if (_enemyType == EnemyType.Dynamic)
            transform.position = _currentWaypoint.position;
            _currentWaypoint = _waypoints.GetNextWaypoint(_currentWaypoint);
            _agent.SetDestination(_currentWaypoint.position);
        }else{
            Debug.Log(message:"Attach Waypoints script and its respective waypoint prefab to the script ");
        }
    }
}
