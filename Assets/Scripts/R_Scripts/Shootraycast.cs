using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shootraycast : StateMachineContext
{
#region Raycast
    [SerializeField][Header("Raycast settings")]
    private RaycastHit hit;
    [SerializeField] protected Vector3 _boxcastSize = new Vector3(10f, 10f, 10f);
    [SerializeField] protected float _raycastDistance = 10f;
    [SerializeField] protected float _verticalOffset = 1f;
#endregion
#region NavMeshAgent
    protected Vector3 _targetPosition;
    protected PlayerController _playerController;
    protected IsPlayerSafe _isPlayer;
    protected TurnOnOffLight _turnOnOffLight;
    protected Component _objectComponent = null;
    protected bool _targetIsLocked = false;
#endregion 
    public bool ShootRaycast(List<string> tagList)    
    {
        Vector3 raycastOrigin = transform.position + Vector3.up * _verticalOffset;
        if (Physics.BoxCast(raycastOrigin, _boxcastSize / 2f, transform.forward, out hit, transform.rotation, _raycastDistance))
        {
            foreach (string tag in tagList)
            {
                if (hit.collider.CompareTag(tag))
                {
                    _targetIsLocked = true;
                    _targetPosition = hit.collider.gameObject.transform.position;

                    if (hit.collider.GetComponent<PlayerController>() != null)
                    {
                        _objectComponent = GetComponent<PlayerController>();
                        _isPlayer = GetComponent<IsPlayerSafe>();

                    }
                    if (hit.collider.GetComponent<TurnOnOffLight>() != null)
                    {
                        _objectComponent = GetComponent<TurnOnOffLight>();
                    }
                }
            }
        }
        return _targetIsLocked;   
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 raycastOrigin = transform.position + Vector3.up * _verticalOffset;
        Gizmos.DrawWireCube(raycastOrigin + transform.forward * _raycastDistance / 2f, _boxcastSize);
        Gizmos.DrawRay(raycastOrigin, transform.forward * _raycastDistance);
    }
}