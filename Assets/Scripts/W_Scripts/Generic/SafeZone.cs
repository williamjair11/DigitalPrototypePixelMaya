using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SafeZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        GameManager.Instance.playerController.IsPlayerInSafeZone = true;
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        GameManager.Instance.playerController.IsPlayerInSafeZone = false;
    }
}
