using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(InputController))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private float _cameraSensitivity= 30f;
    [SerializeField] Transform _cameraTransform= null;
    InputController _inputController= null;
    [SerializeField] Transform _player;
    [SerializeField] private  float _positiveAngleLimit = 90f;
    [SerializeField] private float _negativeAngleLimit = -90f;
    private float rotationX;

    public quaternion DEFAULT_CAMERA;
    

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

        rotationX -= input.y;
        rotationX = Mathf.Clamp(rotationX, _negativeAngleLimit, _positiveAngleLimit);

        transform.Rotate(Vector3.up * input.x * _cameraSensitivity * Time.deltaTime, Space.World);       
        //_cameraTransform.Rotate(Vector3.right * -input.y * _cameraSensitivity * Time.deltaTime, Space.Self);
        _cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }

    public void DefaultCameraState() 
    {
        _cameraTransform.rotation = (DEFAULT_CAMERA);        
    }
}
