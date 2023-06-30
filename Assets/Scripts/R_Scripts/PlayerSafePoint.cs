using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSafePoint : MonoBehaviour
{
   private bool _playerIsInsideGreenLight; 
    [SerializeField] private EnemyMovement _enemyMovement;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("greenTorch"))
        {
            _playerIsInsideGreenLight = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("greenTorch"))
        {
            _playerIsInsideGreenLight = false;
        }
    }
    public bool IsGameObjectInsideGreenTorch()
    {
        return _playerIsInsideGreenLight;
    }
}
