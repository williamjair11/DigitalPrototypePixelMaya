using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private float _shakeDuration = .5f, _shakeXIntensity = .2f, _shakeYIntensity = .2f;
    private Vector3 _initialPosition;


    public bool CameraIsMoving {get; set;}
    private Vector2 _cameraInput = Vector2.zero;
    public Vector2 CameraInput {get => _cameraInput; set => _cameraInput = value;}
    private float rotationX;
    
    private void Awake()
    {
        rotationX = _camera.transform.rotation.x;
        _initialPosition = transform.localPosition;
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

    public void ShakeCamera()
    {
        StartCoroutine(ShakeCameraEnumerator());
    }

    IEnumerator ShakeCameraEnumerator()
    {
        float timeCounter = 0;
        while(_shakeDuration > timeCounter)
        {
            Debug.Log("Shaking");
            float x = Random.Range(-_shakeXIntensity, _shakeXIntensity);
            //float y = Random.Range(-_shakeYIntensity, _shakeYIntensity);
            timeCounter += Time.deltaTime;
            transform.localPosition = new Vector3(x, transform.localPosition.y, _initialPosition.z);
            yield return null;
            
        }
        transform.localPosition = _initialPosition;
        
    }
}
