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
//Este enemigo cambia su comportamiento en función de la distancia del player, la idea es que 
//durante el juego parece un personaje inofensivo, cuando te acercas te saluda e incluso camina hacía ti
//con calma, sin embargo, una vez esta lo suficientemente cerca intenta atacarte
// mas detalles: https://www.notion.so/Jugabilidad-268b6b9661034621823c4e122d401556?pvs=4#99451906cfb3413f862ded327558b0e2
public class Tzitzimime : StateMachineContext
{
    //Variables de configuración
    [SerializeField]
    private float
    _walkSpeed = 2,
    _runSpeed = 5,
    _currentPlayerDistance,
    _distanceToLook = 30,
    _distanceToGreet = 25,
    _distanceToWalk = 20,
    _distanceToFollow = 10,
    _distanceToAttack = 2;
    [SerializeField] private GameObject _target, _player, _initialPosition;
    [SerializeField] private int _idleAnimationsCount = 3;
    [SerializeField] private TzitzimimeStatesId _initialState = TzitzimimeStatesId.Idle;
    [SerializeField] private TzitzimimeStatesId  _currentEnemyState;
    private bool _playerIsInGreetingRange = false, _isWalkingToInitialPlace = false;

    //Componentes y referencias
    Animator _animator;
    NavMeshAgent _agent;

    //Getters y Setters de componentes y referencias
    public Animator Animator { get => _animator; }
    private HealtController _healtController;
    public NavMeshAgent Agent{get=>_agent;}

    //Getters y Setters 
    public float CurrentPlayerDistance { get => _currentPlayerDistance; }
    public float DistanceToLook { get => _distanceToLook; }
    public float DistanceToGreet { get => _distanceToGreet; }
    public float DistanceToWalk { get => _distanceToWalk; }
    public float DistanceToFollow { get => _distanceToFollow; }
    public float DistanceToAttack { get => _distanceToAttack; }
    public int IdleAnimationsCount { get => _idleAnimationsCount; }
    public float RunSpeed { get => _runSpeed; }
    public float WalkSpeed { get => _walkSpeed; }
    public GameObject Target { get => _target; }
    public TzitzimimeStatesId CurrentEnemyState {get => _currentEnemyState;}
    public bool TargetIsInGreetingRange { get => _playerIsInGreetingRange; set => _playerIsInGreetingRange = value; }
    public bool IsWalkingToInitialPlace { get => _isWalkingToInitialPlace;}
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
        _currentPlayerDistance = Vector3.Distance(_player.transform.position, transform.position);
        SetStateByPlayerDistance();
        //Es importante colocar _currentSate.Update al final de todas las lineas de código
        _currentState.Update();
    }

    //Actualiza el estado actual del enemigo en función de la distancia con el target
    void SetStateByPlayerDistance()
    {
        
        if (_currentPlayerDistance > _distanceToWalk)
        {
            _playerIsInGreetingRange = false;
            
            if(Vector3.Distance(transform.position, _initialPosition.transform.position) > 1) 
            {   
                _target = _initialPosition;
                _isWalkingToInitialPlace = true;
                _currentEnemyState = TzitzimimeStatesId.Walking;
                Debug.Log("walking");
            }
            else
            { 
                _isWalkingToInitialPlace = false;
                _target = _player;
                _currentEnemyState = TzitzimimeStatesId.Idle;  
            }
        }

        if (_currentPlayerDistance <= _distanceToWalk && _currentPlayerDistance > DistanceToFollow)
            _currentEnemyState = TzitzimimeStatesId.Walking;

        if(_target == _initialPosition) return;
        if (_currentPlayerDistance <= DistanceToGreet && _currentPlayerDistance > _distanceToWalk)
        {
            _playerIsInGreetingRange = true;
            _currentEnemyState = TzitzimimeStatesId.Idle;
        }
 
        if (_currentPlayerDistance <= DistanceToFollow && _currentPlayerDistance > _distanceToAttack)
            _currentEnemyState = TzitzimimeStatesId.Following;
        

        if (_currentPlayerDistance <= DistanceToAttack)
            _currentEnemyState = TzitzimimeStatesId.Attacking;
            
    }

}
