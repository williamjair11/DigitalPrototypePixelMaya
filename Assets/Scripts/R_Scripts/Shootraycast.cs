using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootraycast : MonoBehaviour
{
    #region Raycast
    
    [Header("Raycast settings")]
    private RaycastHit hit;
    [SerializeField] private float _raycastRadius = 5f;
    [SerializeField] private float _raycastDistance = 15f;
    [SerializeField] private float _verticalOffset = 1f;
    #endregion
    #region Getters and setters
    public float  RaycastRadius { get { return _raycastRadius; } set { _raycastRadius = value; } }    
    public float  RaycastDistance { get { return _raycastDistance; } set { _raycastRadius = value; } }    
    public float  VerticalOffset { get { return _verticalOffset; } set { _raycastRadius = value; } }    
    #endregion
    public bool ShootRaycast(List<string> gameObjectTagList)
    {
        bool raycastHit = false;
        Vector3 raycastOrigin = transform.position + Vector3.up * _verticalOffset;
        if (Physics.SphereCast(raycastOrigin, _raycastRadius, transform.forward, out hit, _raycastDistance))
        {
            foreach (string gameObject in gameObjectTagList)
            {
                if(hit.collider.CompareTag(gameObject))
                {
                    raycastHit = true;
                }
            }
        }
        return raycastHit;   
    }
}