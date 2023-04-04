using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerDetection : MonoBehaviour
{
    public NavMeshAgent _enemy;
    public GameObject _player;
    public Vector3 _LastPlayerPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_LastPlayerPosition != _player.transform.position){
            _enemy.destination = _player.transform.position;
        }
        _LastPlayerPosition = _player.transform.position;
        
    }
}
