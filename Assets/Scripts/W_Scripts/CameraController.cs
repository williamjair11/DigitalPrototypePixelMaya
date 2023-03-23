using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(InputController))]
public class CameraController : MonoBehaviour
{
    [SerializeField] float _cameraSensitivity= 30f;
    [SerializeField] Transform _cameraTransform= null;
    InputController _inputController= null;

    private void Awake()
    {
        _inputController = GetComponent<InputController>();
    }
    void Update()
    {
        MoveCamera();
    }
    void MoveCamera() 
    {
        Vector2 input = _inputController.CameraInput();

        transform.Rotate(Vector3.up * input.x * _cameraSensitivity * Time.deltaTime);

        Vector3 angle = _cameraTransform.eulerAngles;
        angle.x += -input.y * _cameraSensitivity * Time.deltaTime;

        _cameraTransform.eulerAngles = angle;
    }
}
