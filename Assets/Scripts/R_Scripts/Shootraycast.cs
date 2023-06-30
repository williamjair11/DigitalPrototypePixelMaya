using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootraycast : MonoBehaviour
{
    #region Raycast
    
    [Header("Raycast settings")]
    private RaycastHit hit;
    [SerializeField] private float raycastRadius = 5f;
    [SerializeField] private float raycastDistance = 15f;
    [SerializeField] private float verticalOffset = 1f;
    #endregion

    EnergyBallScript _energyBallScript;
    public bool ShootRaycast(string gameObjectTag)
    {
        bool raycastHit = false;
        Vector3 raycastOrigin = transform.position + Vector3.up * verticalOffset;
        if (Physics.SphereCast(raycastOrigin, raycastRadius, transform.forward, out hit, raycastDistance))
        {
            if(hit.collider.CompareTag(gameObjectTag))
            {
                raycastHit = true;
            }
        }
        return raycastHit;   
    }
    public Vector3 GetWhiteLightPosition(string lightTag)
    {
        Vector3 lightSourcePosition = Vector3.zero;
        Vector3 raycastOrigin = transform.position + Vector3.up * verticalOffset;
        if (Physics.SphereCast(raycastOrigin, raycastRadius, transform.forward, out hit, raycastDistance))
        {
            if(hit.collider.CompareTag(lightTag) && hit.collider.gameObject != null)
            {
                _energyBallScript = hit.collider.GetComponent<EnergyBallScript>();
                lightSourcePosition = _energyBallScript.SavePosition();
                
                
            }
        }
        return lightSourcePosition;  
    }
}