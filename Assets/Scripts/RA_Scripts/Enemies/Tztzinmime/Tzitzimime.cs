using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using DG.Tweening;

//Una lista con los distintos states id del enemigo para evitar errores 
//de escritura al manejar strings
public enum TzitzimimeStatesId
{
    Walking,
    Following,
    Idle,
    Attacking,
    Stuned
}

public enum TzitzimimeAnimationsId
{
    Walking,
    Following,
    Idle,
    Greeting,
    Running,
    Attacking,
    ReciveDamage,
    Stuned
}

[RequireComponent( typeof(NavMeshAgent))]
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
    _distanceToAttack = 2,
    _stunedTime = 5;
    [SerializeField] private GameObject _target, _player, _targetPoint;
    [SerializeField] private DamageController _damageZone;
    [SerializeField] private int _idleAnimationsCount = 3;
    [SerializeField] private TzitzimimeStatesId _initialState = TzitzimimeStatesId.Idle;
    [SerializeField] private TzitzimimeStatesId  _currentEnemyStateId;
    private bool _playerIsInGreetingRange = false, _isWalkingToTargetPoint = false;
    [SerializeField] bool _canMakeDamage, _isStuned;

    //Componentes y referencias
    Animator _animator;
    NavMeshAgent _agent;
    
    //Getters y Setters 
    public Animator Animator { get => _animator; }
    public NavMeshAgent Agent{get=>_agent;}
    public TzitzimimeStatesId CurrentEnemyState {get => _currentEnemyStateId;}
    public GameObject Target { get => _target; }
    public float CurrentPlayerDistance { get => _currentPlayerDistance; }
    public float DistanceToLook { get => _distanceToLook; }
    public float DistanceToGreet { get => _distanceToGreet; }
    public float DistanceToWalk { get => _distanceToWalk; }
    public float DistanceToFollow { get => _distanceToFollow; }
    public float DistanceToAttack { get => _distanceToAttack; }
    public int IdleAnimationsCount { get => _idleAnimationsCount; }
    public float RunSpeed { get => _runSpeed; }
    public float WalkSpeed { get => _walkSpeed; }    
    public bool CanMakeDamage { get => _canMakeDamage;}
    public bool TargetIsInGreetingRange { get => _playerIsInGreetingRange; set => _playerIsInGreetingRange = value; }
    public bool IsWalkingToTargetPoint { get => _isWalkingToTargetPoint;}
    public bool IsStuned { get => _isStuned; set => _isStuned = value;}
    public float StunedTime { get => _stunedTime; } 


// Los métodos EnableCanMakeDamage y DisableCanMakeDamage cambian a true o false la variable 
// _canMakeDamage la cual determina si el enemigo puede hacer daño o no
// Estos métodos pueden ser usados en events o AnimationEvents para ajustar el momento exacto en el que se
// debe activar o desactivar el daño. En este caso por ejemplo el daño del enemigo se activa en un momento
//exacto de la animación de ataque mediante animationEvents
    
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        DisableCanMakeDamage();
    }

    void Start()
    {
        InitializeStateMachine();
        _agent.SetDestination(_target.transform.position);
        _agent.stoppingDistance = 0;
        _agent.speed = _walkSpeed;
    }

    public void EnableCanMakeDamage()
    {
        _damageZone.CanMakeDamage = true;
        _canMakeDamage = true;
    }

    public void DisableCanMakeDamage()
    {
        _damageZone.CanMakeDamage = false;
        _canMakeDamage = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnergyAttack"))
        {
            _isStuned = true;
            _currentState.SwitchState(_stateFactory.GetState(TzitzimimeStatesId.Stuned.ToString()));
        }
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
        if(!_isStuned) SetStateByPlayerDistance();

        //Es importante colocar _currentSate.Update al final de todas las lineas de código
        _currentState.Update();
    }

    public void IgnorePlayer()
    {
        if(Vector3.Distance(transform.position, _targetPoint.transform.position) > 1) 
            {   
                _target = _targetPoint;
                _isWalkingToTargetPoint = true;
                _currentEnemyStateId = TzitzimimeStatesId.Walking;
            }
            else
            { 
                
                    _isWalkingToTargetPoint = false;
                    if(!GameManager.Instance.playerController.IsPlayerInSafeZone)
                    _target = _player;
                    _currentEnemyStateId = TzitzimimeStatesId.Idle; 
            }
    }

    //Actualiza el estado actual del enemigo en función de la distancia con el target
    void SetStateByPlayerDistance()
    {
        if(GameManager.Instance.playerController.IsPlayerInSafeZone)
        {
            IgnorePlayer();
            return;
        }
        
        if (_currentPlayerDistance > _distanceToWalk)
        {
            _playerIsInGreetingRange = false;
            IgnorePlayer();
        }

        if (_currentPlayerDistance <= _distanceToWalk && _currentPlayerDistance > DistanceToFollow)
        {    
            _isWalkingToTargetPoint = false;
            _target = _player;
            _currentEnemyStateId = TzitzimimeStatesId.Walking;
        }

        if (_currentPlayerDistance <= DistanceToGreet && _currentPlayerDistance > _distanceToWalk)
        {
            _playerIsInGreetingRange = true;
            _currentEnemyStateId = TzitzimimeStatesId.Idle;
        }
 
        if (_currentPlayerDistance <= DistanceToFollow && _currentPlayerDistance > _distanceToAttack)
            _currentEnemyStateId = TzitzimimeStatesId.Following;
        

        if (_currentPlayerDistance <= DistanceToAttack)
            _currentEnemyStateId = TzitzimimeStatesId.Attacking;
            
    }

}
