using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera states")]
    public bool isActivated = true;
    [SerializeField] private GameObject _playerGameObject;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _cameraSensitivityX= 30f;
    [SerializeField] private float _cameraSensitivityY = 30f;
    [SerializeField] private float _positiveAngleLimit = 90f;
    [SerializeField] private float _negativeAngleLimit = -90f;
    private float rotationX;

    [Header("References")]
    InputController _inputController= null;

    
    private void Awake()
    {
        _inputController = FindObjectOfType<InputController>();
        rotationX = _camera.transform.rotation.x;
    }
    void Update()
    {
        if (isActivated) 
        {
            MoveCamera();
        }             
    }
    void MoveCamera() 
    {
        Vector2 input = _inputController.CameraInput();

        rotationX -= input.y * _cameraSensitivityY;
        rotationX = Mathf.Clamp(rotationX, _negativeAngleLimit, _positiveAngleLimit);
       
        _playerGameObject.transform.Rotate(Vector3.up * input.x * _cameraSensitivityX * Time.deltaTime, Space.World);       
        _camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
}
