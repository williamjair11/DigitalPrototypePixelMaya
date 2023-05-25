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
    [SerializeField] private float _cameraSensitivity= 30f;
    [SerializeField] private float _positiveAngleLimit = 90f;
    [SerializeField] private float _negativeAngleLimit = -90f;
    [SerializeField] Transform _cameraTransform= null;
    public quaternion DEFAULT_CAMERA;
    private float rotationX;
    private float rotationY;

    [Header("References")]
    InputController _inputController= null;
    [SerializeField] Transform _player;
    
    private void Awake()
    {
        _inputController = GetComponent<InputController>();
        DEFAULT_CAMERA = transform.rotation;
        rotationX = _cameraTransform.rotation.x;
        rotationY = _cameraTransform.rotation.y;

    }
    void Update()
    {
        MoveCamera();
        
    }
    void MoveCamera() 
    {
        Vector2 input = _inputController.CameraInput();

        rotationX -= input.y * 0.5f;
        rotationY -= input.x;
        rotationX = Mathf.Clamp(rotationX, _negativeAngleLimit, _positiveAngleLimit);
        
        transform.Rotate(Vector3.up * input.x * _cameraSensitivity * Time.deltaTime, Space.World);       
        //_cameraTransform.Rotate(Vector3.left * input.x * Time.deltaTime, Space.Self);

        _cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }

    public void DefaultCameraState() 
    {
        _cameraTransform.rotation = (DEFAULT_CAMERA);        
    }
}
