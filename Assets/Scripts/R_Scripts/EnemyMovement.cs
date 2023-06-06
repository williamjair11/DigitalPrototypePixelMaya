using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    //Animation states
    const string ENEMY_IS_WALKING = "isWalking";
    const string ENEMY_IS_RUNNING = "isRunning";
    const string ENEMY_IS_ROTATING = "isRotating";
    const string ENEMY_IS_IDLE = "isIdle";
    const string ENEMY_IS_ATACKING = "isAtacking";
    AnimationController _enemyAnimatorController;
    [HideInInspector] public Transform _currentWaypoint;
    [SerializeField] private Waypoints _waypoint;
    [HideInInspector] public NavMeshAgent _enemy;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _atackingDistance = 1f;
    [SerializeField] private float _trackingDistance = 10f; //The distance at which the enemy will search for the player 
    [SerializeField] private float runningSpeed = 6f;
    [SerializeField] private float walkingSpeed = 3f;
    [HideInInspector] private float distanceThreshold = 0.1f; //distance threshold for the the waypoints
    private Vector3 _playerLastPosition;
    private float distance;
    private bool _playerMoved;    
    private bool _canMove = true;
    private bool _enemyDetectsLight = false;
    private Vector3 _lightTarget;
    private bool _playerIsInsideGreenLight = false;
    private bool _onLightOrbDetection = false;
    private GameObject _energyBallClone;
    private bool _onWhiteTorchDetection = false;

    void Start() 
    {
        _enemyAnimatorController = GetComponent<AnimationController>();
        _enemy = GetComponent<NavMeshAgent>();
        _playerLastPosition = _playerController.savePosition();

        //Set initial position to the first waypoint
        _currentWaypoint = _waypoint.GetNextWaypoint(_currentWaypoint);
        transform.position = _currentWaypoint.position;
        
        //Set the next waypoint target
        _currentWaypoint = _waypoint.GetNextWaypoint(_currentWaypoint);
    }
    void Update()
    {
        _playerLastPosition = _playerController.savePosition();
        GetEnemyState();
        if (_canMove)
        {
            if (_onWhiteTorchDetection)
            {
                _enemy.destination = GameObject.Find("TorchLighting (1)").transform.position;
                _enemy.speed = runningSpeed;
            }
            else if (_onLightOrbDetection)
            {
                _enemy.destination = GameObject.Find("EnergyBall(Clone)").transform.position;
                _enemy.speed = runningSpeed;
            }

            else if (_playerIsInsideGreenLight)
            {
                _enemy.speed = walkingSpeed;
                _enemy.destination = _currentWaypoint.position;
            }

            if (Vector3.Distance(_enemy.transform.position, _playerLastPosition) > _trackingDistance) //Enemy follows waypoints
            {
                _enemy.speed = walkingSpeed;
                _enemy.destination = _currentWaypoint.position;
                if (!_enemy.isStopped)
                {
                    if (Vector3.Distance(transform.position, _currentWaypoint.position) < distanceThreshold)
                    {
                        _currentWaypoint = _waypoint.GetNextWaypoint(_currentWaypoint);
                        _enemy.destination = _currentWaypoint.position;
                        _enemy.speed = 0f;
                    }
                }
            }
            else if (Vector3.Distance(_enemy.transform.position, _playerLastPosition) < _trackingDistance)
            {
                if (Vector3.Distance(_enemy.transform.position, _playerLastPosition) <= 2f)
                {
                    _enemy.speed = 0f;
                    _enemyAnimatorController._animator.Play("Enemy_Atack");
                }
                else
                {
                    _enemy.speed = runningSpeed;
                    _enemy.destination = _playerLastPosition;
                    _enemy.speed = runningSpeed;
                }
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
        else if (_enemy.speed <= 0.1){
            _enemyAnimatorController.ChangeAnimationStateTo(ENEMY_IS_IDLE);
            return EnemyState.Idle;
        }
        else if (_enemy.speed > 0 && Vector3.Angle(_enemy.velocity, _enemy.transform.forward) > 10f){
            _enemyAnimatorController.ChangeAnimationStateTo(ENEMY_IS_ROTATING);
            return EnemyState.Turning;
        }
        else if (Vector3.Distance(_enemy.transform.position, _playerLastPosition) < _atackingDistance)
        {
            _enemy.isStopped = true;
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
    IEnumerator RandomDelay()
    {
        float _randomDelay = Random.Range(3.0f, 5.0f);
        if (Vector3.Distance(_enemy.transform.position, _currentWaypoint.position) < distanceThreshold + 0.1f)
        {
            _enemy.speed = 0f;;
        }
        yield return new WaitForSeconds(_randomDelay);
        _enemy.speed = walkingSpeed;
    }
        private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("greenTorch"))
        {
            _playerIsInsideGreenLight = true;
        }
        else
        {
            _playerIsInsideGreenLight = false;
        }
        if (other.CompareTag("playerLightOrb"))
        {
            _onLightOrbDetection = true;
        }
        else
        {
            _onLightOrbDetection = false;
        }
        if (other.CompareTag("whiteTorch"))
        {
            _onWhiteTorchDetection = true;
        }
        else
        {
            _onWhiteTorchDetection = false;
        }
        if (other.CompareTag("whiteTorchOff"))
        {
            Destroy(GameObject.Find("TorchLighting(1)"));
        }
        if (other.CompareTag("destroyOrb"))
        {
            Destroy(GameObject.Find("EnergyBall(Clone)"));
        }    
    }
}
