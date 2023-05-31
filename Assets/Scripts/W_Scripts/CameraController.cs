using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

[RequireComponent(typeof(InputController))]
public class CameraController : MonoBehaviour
{
    [Header("Camera states")]
    [SerializeField] private float _cameraSensitivityX= 30f;
    [SerializeField] private float _cameraSensitivityY = 30f;
    [SerializeField] private float _positiveAngleLimit = 90f;
    [SerializeField] private float _negativeAngleLimit = -90f;
    [SerializeField] Transform _cameraTransform= null;
    [SerializeField] private quaternion DEFAULT_CAMERA;
    private float rotationX;

    [Header("References")]
    InputController _inputController= null;
    [SerializeField] Transform _player;
    
    private void Awake()
    {
        _inputController = GetComponent<InputController>();
        DEFAULT_CAMERA = transform.rotation;
        rotationX = _cameraTransform.rotation.x;
    }
    void Update()
    {
        MoveCamera();       
    }
    void MoveCamera() 
    {
        Vector2 input = _inputController.CameraInput();

        rotationX -= input.y * _cameraSensitivityY;
        rotationX = Mathf.Clamp(rotationX, _negativeAngleLimit, _positiveAngleLimit);
       
        transform.Rotate(Vector3.up * input.x * _cameraSensitivityX * Time.deltaTime, Space.World);       
        _cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }

    public quaternion DefaultCameraState() 
    {
        return DEFAULT_CAMERA;      
    }
}
