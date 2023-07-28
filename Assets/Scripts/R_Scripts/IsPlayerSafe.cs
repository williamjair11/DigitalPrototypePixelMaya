using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerSafe : MonoBehaviour
{
    CapsuleCollider _capsulCollider;
    [SerializeField] public bool _safe;
    private void Awake() {
        _capsulCollider = GetComponent<CapsuleCollider>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("greenTorch"))
        {
            _safe = true;

        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("greenTorch"))
        {
            _safe = false;
        }
    }
}
