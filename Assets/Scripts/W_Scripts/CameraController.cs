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


    public bool CameraIsMoving {get; set;}
    private Vector2 _cameraInput = Vector2.zero;
    public Vector2 CameraInput {get => _cameraInput; set => _cameraInput = value;}
    private float rotationX;
    
    private void Awake()
    {
        rotationX = _camera.transform.rotation.x;
    }

    void LateUpdate()
    {
        if(CameraIsMoving) MoveCamera();

        if(CameraIsMoving && GameManager.Instance.CurrentControlType != ControlType.Vr)
        GameManager.Instance.uIController.HideShortMenu();
    }

    public void MoveCamera() 
    {
        if(!isActivated) return;
        rotationX -= CameraInput.y * _cameraSensitivityY;
        rotationX = Mathf.Clamp(rotationX, _negativeAngleLimit, _positiveAngleLimit);
       
        _playerGameObject.transform.Rotate(Vector3.up * CameraInput.x * _cameraSensitivityX * Time.deltaTime, Space.World);       
        _camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
}
