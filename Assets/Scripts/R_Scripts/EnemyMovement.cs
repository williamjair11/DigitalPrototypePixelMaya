using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent _enemy;
    private Vector3 _lastPlayerPosition;
    private float distance;
    private bool _playerMoved;
    [SerializeField] private float tracksPlayerWhitinRangeOf;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float walkingSpeed;
    [SerializeField] private PlayerController _playerController;
    public GameObject _enemyCheckPoint1;
    public GameObject _enemyCheckPoint2;
    LightDetection _lightDetection;
    private bool _canMove = true;
    [SerializeField] private List<GameObject> checkPointList = new List<GameObject>();
    [SerializeField] private GameObject checkPointSpawnPosition;

    void Start() {
        _lightDetection = GetComponent<LightDetection>();
        _enemy = GetComponent<NavMeshAgent>();
        _lastPlayerPosition = _playerController.savePosition();
        // transform.position = _enemyCheckPoint1.transform.position;
    }
    void Update()
    {
        if (_canMove)
        {
            if (InitialPlayerPositionChange())
            {
                if (DistanceBetween(_enemy, _lastPlayerPosition) > tracksPlayerWhitinRangeOf)
                {
                    _enemy.destination = _enemyCheckPoint1.transform.position;
                    _enemy.speed = walkingSpeed;
                    if (_enemy.transform.position == _enemyCheckPoint1.transform.position)
                    {
                        _enemy.destination = _enemyCheckPoint2.transform.position;
                    }
                    else if (_enemy.destination == _enemyCheckPoint2.transform.position)
                    {
                        _enemy.destination = _enemyCheckPoint1.transform.position;
                    }
                }
                else
                {
                    if (DistanceBetween(_enemy, _lastPlayerPosition) == 1f )
                    {
                        _enemy.speed = 0f;
                    }
                    else
                    {
                        _enemy.destination = _lastPlayerPosition;
                        _enemy.speed = runningSpeed;
                    }

                }
            }
            _lastPlayerPosition = _playerController.savePosition();
            GetEnemyState();
        }
        CreateCheckpoints();
            
    }
    /// <summary>
    /// Returns the distance between two objects as a float.
    /// </summary>
    /// <param name="_navMeshAgent">The first object to compare.</param>
    /// <param name="_vector3">The second object to compare.</param>
    public float DistanceBetween(NavMeshAgent _navMeshAgent, Vector3 _vector3)
    {
        distance = Vector3.Distance(_navMeshAgent.transform.position, _vector3);
        Debug.Log(distance);
        return distance;
    }
    public enum EnemyState 
    {
        Walking,
        Running,
        Idle,
        Turning,
        BesidePlayer
    }
    public EnemyState GetEnemyState() 
    {
        if (_enemy.speed == walkingSpeed) {
            return EnemyState.Walking;
        }
        else if (_enemy.speed == runningSpeed) {
            return EnemyState.Running;
        }
        else if (_enemy.speed == 0f){
            return EnemyState.Idle;
        }
        else if (_enemy.velocity.magnitude > 0 && Vector3.Angle(_enemy.velocity, _enemy.transform.forward) > 10f)
        {
            return EnemyState.Turning;
        }
        else if (DistanceBetween(_enemy, _lastPlayerPosition) == 2f)
        {
            return EnemyState.BesidePlayer;
        }
        else 
        {
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
    private bool InitialPlayerPositionChange()
    {
        if (_lastPlayerPosition != _playerController.savePosition())
        {
            _playerMoved = true;
        }
        else
        {
            _playerMoved = false;
        }
        return _playerMoved;
    }
    private void CreateCheckpoints()
    {
        foreach (GameObject obj in checkPointList)
        {
            Instantiate(obj);
        }
    }

}
