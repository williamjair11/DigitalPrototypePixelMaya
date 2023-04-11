using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(InputController))]
public class CameraController : MonoBehaviour
{
    [SerializeField] float _cameraSensitivity= 30f;
    [SerializeField] Transform _cameraTransform= null;
    InputController _inputController= null;
    [SerializeField] Transform _player;

    public quaternion DEFAULT_CAMERA;
    

    private void Awake()
    {
        _inputController = GetComponent<InputController>();
        DEFAULT_CAMERA = transform.rotation;
    }
    void Update()
    {
        MoveCamera();
        
    }
    void MoveCamera() 
    {
        Vector2 input = _inputController.CameraInput();

        transform.Rotate(Vector3.up * input.x * _cameraSensitivity * Time.deltaTime, Space.World);
        _cameraTransform.Rotate(Vector3.right * -input.y * _cameraSensitivity * Time.deltaTime, Space.Self);      
    }

    public void DefaultCameraState() 
    {
        _cameraTransform.rotation = (DEFAULT_CAMERA);        
    }
}
