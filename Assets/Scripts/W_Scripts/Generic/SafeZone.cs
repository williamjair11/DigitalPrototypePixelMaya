using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SafeZone : MonoBehaviour
{
    [SerializeField] bool _isEnabled;

    public bool IsEnabled
    {
        set => _isEnabled = value;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && _isEnabled)
        GameManager.Instance.playerController.IsPlayerInSafeZone = true;
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        GameManager.Instance.playerController.IsPlayerInSafeZone = false;
    }
}
