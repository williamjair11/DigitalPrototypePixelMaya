using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSafePoint : MonoBehaviour
{
    EnemyMovementRefactor _enemyMovement;
    private void Awake() {
        _enemyMovement = FindObjectOfType<EnemyMovementRefactor>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("greenTorch"))
        {
            _enemyMovement._playerIsInsideGreenLight = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("greenTorch"))
        {
            _enemyMovement._playerIsInsideGreenLight = false;
        }
    }
}
