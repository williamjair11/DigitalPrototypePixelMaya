using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

//Una lista con los distintos states id del enemigo para evitar errores 
//de escritura al manejar strings
public enum TzitzimimeStatesId
{
    Walking,
    Following,
    Idle,
    Attacking
}

public enum TzitzimimeAnimationsId
{
    Walking,
    Following,
    Idle,
    Greeting,
    Running,
    Attacking
}

[RequireComponent(typeof(HealtController), typeof(NavMeshAgent))]

public class Tzitzimime : StateMachineContext
{
    [SerializeField]
    private float
    _walkSpeed = 2,
    _runSpeed = 5,
    _currentTargetDistance,
    _distanceToLook = 30,
    _distanceToGreet = 25,
    _distanceToWalk = 20,
    _distanceToFollow = 10,
    _distanceToAttack = 2;

    [SerializeField] private int _idleAnimationsCount = 3;
    public int IdleAnimationsCount { get => _idleAnimationsCount; }
    public float RunSpeed { get => _runSpeed; }
    public float WalkSpeed { get => _walkSpeed; }

    Animator _animator;
    public Animator Animator { get => _animator; }
    public float CurrentTargetDistance { get => _currentTargetDistance; }
    public float DistanceToLook { get => _distanceToLook; }
    public float DistanceToGreet { get => _distanceToGreet; }
    public float DistanceToWalk { get => _distanceToWalk; }
    public float DistanceToFollow { get => _distanceToFollow; }
    public float DistanceToAttack { get => _distanceToAttack; }
    public GameObject Target { get => _target; }

    private bool _greeting = false;
    private bool _walking = false;
    private bool _following = false;
    private bool _looking = false;
    private bool _attacking = false;

    public bool Greeting { get => _greeting; set => _greeting = value; }
    public bool Walking { get => _walking; set => _walking = value; }
    public bool Following { get => _following; set => _following = value; }
    public bool Looking { get => _looking; set => _looking = value; }
    public bool Attacking { get => _attacking; set => _attacking = value; }
    [SerializeField] private GameObject _target;
    private HealtController _healtController;
    [SerializeField]
    private TzitzimimeStatesId
    _initialState = TzitzimimeStatesId.Idle;

    NavMeshAgent _agent;
    public NavMeshAgent Agent{get=>_agent;}

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        InitializeStateMachine();
        _agent.SetDestination(_target.transform.position);
        _agent.stoppingDistance = 0;
        _agent.speed = _walkSpeed;
    }

    public override void InitializeStateMachine()
    {
        //Guarda en una variable la instancia de StateFactory
        _stateFactory = new TzitzimimeStateFactory(this);
        //guarda el initial state como deafult state del state machine (estado 
        //que siempre tomara en caso de un error)
        _stateFactory.DefaultStateId = _initialState.ToString();
        //Iniciamos el primer stado.
        _currentState = _stateFactory.GetState(_initialState.ToString());
        _currentState.OnStartState();
    }

    void Update()
    {
        _currentTargetDistance =
        Vector3.Distance(_target.transform.position, transform.position);
        CheckPlayerDistance();
        //Es importante colocar _currentSate.Update al final de todas las lineas de código
        _currentState.Update();
    }

    void CheckPlayerDistance()
    {
        if (_currentTargetDistance > _distanceToWalk)
        {
            _looking = true;
            _agent.isStopped = true;
            transform.LookAt(_target.transform.position);
        }
        else _looking = false;

        if (_currentTargetDistance <= DistanceToGreet && _currentTargetDistance > _distanceToWalk)
        {
            _greeting = true;
            _agent.isStopped = true;
            transform.LookAt(_target.transform.position);
        }
        else _greeting = false;

        if (_currentTargetDistance <= _distanceToWalk && _currentTargetDistance > DistanceToFollow)
        {
            _agent.SetDestination(_target.transform.position);
            _walking = true;
            _agent.isStopped = false;
        }
        else _walking = false;

        if (_currentTargetDistance <= DistanceToFollow && _currentTargetDistance > _distanceToAttack)
        {
            _agent.SetDestination(_target.transform.position);
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

}
