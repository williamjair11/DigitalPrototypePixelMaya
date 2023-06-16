using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSafePoint : MonoBehaviour
{
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private Test _test;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("greenTorch"))
        {
            _enemyMovement._playerIsInsideGreenLight = true;
            _test._playerIsInsideGreenLight = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("greenTorch"))
        {
            _enemyMovement._playerIsInsideGreenLight = false;
            _test._playerIsInsideGreenLight = false;
        }
    }
}
