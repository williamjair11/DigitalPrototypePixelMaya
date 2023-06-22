using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GreenBallEnergy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _timeStun;

    [SerializeField] private UnityEvent<float> _stunEnemy; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy") 
        {
            other.GetComponent<EnemyMovementRefactor>().stun(_timeStun);
        }
    }
}
