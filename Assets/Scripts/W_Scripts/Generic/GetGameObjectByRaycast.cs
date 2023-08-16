using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum VectorDirections
{
    Forward,
    Up,
    Down,
    Rigth,
    Left,
    Back
}
public class GetGameObjectByRaycast : MonoBehaviour
{
    [SerializeField] VectorDirections direction = VectorDirections.Forward;
    [SerializeField] float _rayDistance;
    [SerializeField] LayerMask _layerMask;

    private Vector3 _raycastDirection;
    private Vector3 _lastHitPoint;

    public Vector3 LastHitPoint{get => _lastHitPoint;}

    void Start()
    {
        SetRaycastDirection(direction);
    }

    public GameObject ThrowRaycast()
    {
        GameObject hitObject = null;

        Ray ray = new Ray(gameObject.transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _rayDistance, _layerMask))
        {
            hitObject = hit.transform.gameObject;
            _lastHitPoint = hit.point;
        }
        return hitObject;
    }

    public void SetRaycastDirection(VectorDirections newDirection)
    {
        switch (newDirection)
        {
            case VectorDirections.Forward:
                _raycastDirection = Vector3.forward;
                break;
            case VectorDirections.Back:
                _raycastDirection = Vector3.back;
                break;
            case VectorDirections.Rigth:
                _raycastDirection = Vector3.right;
                break;
            case VectorDirections.Left:
                _raycastDirection = Vector3.left;
                break;
            case VectorDirections.Down:
                _raycastDirection = Vector3.down;
                break;
            case VectorDirections.Up:
                _raycastDirection = Vector3.up;
                break;
            default:
                break;
        }
    }
}
