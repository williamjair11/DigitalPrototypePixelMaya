using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] GameObject _player, _respawnPoint;
    public void OnTriggerStay(Collider other)
    {
        Debug.Log("que pedo");
        if(other.CompareTag("Player"))
        {
            Debug.Log("que pedox2");
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        _player.transform.position = _respawnPoint.transform.position;
    }
}
