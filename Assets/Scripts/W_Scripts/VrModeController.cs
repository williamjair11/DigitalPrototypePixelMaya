using System.Collections;
using Google.XR.Cardboard;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class VrModeController : MonoBehaviour
{

    private const float _defaultFieldOfView = 90f;

    private Camera _mainCamera;
    [SerializeField] private InputController _inputController;

    [SerializeField] private CameraController _cameraController;

    private quaternion DEFAULT_CAMERA;


    public void Start()
    {
        _inputController.GetComponent<InputController>();

        _mainCamera = Camera.main;

        _cameraController = new CameraController();
        DEFAULT_CAMERA = _cameraController.DEFAULT_CAMERA;


        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.brightness = 1.0f;

        if (!Api.HasDeviceParams())
        {
            Api.ScanDeviceParams();
        }
    }
    public void Update()
    {
        bool VrIsActivated = _inputController._inputHybridIsActived;
        bool state = true;

        if (Api.IsCloseButtonPressed)
        {
            _inputController.ChangedModeController(0);
            ExitVR();
            state = true;
        }

        if (Api.IsGearButtonPressed)
        {
            Api.ScanDeviceParams();
        }

        Api.UpdateScreenParams();

        if (VrIsActivated)
        {
            if (state)
            {
                EnterVR();
                state = false;
            }
        }
    }

    private void EnterVR()
    {
        StartCoroutine(StartXR());
        if (Api.HasNewDeviceParams())
        {
            Api.ReloadDeviceParams();
        }
    }

    public void ExitVR()
    {
        StopXR();
    }

    private IEnumerator StartXR()
    {
        Debug.Log("Initializing XR...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed.");
        }
        else
        {
            Debug.Log("XR initialized.");

            Debug.Log("Starting XR...");
            XRGeneralSettings.Instance.Manager.StartSubsystems();
            Debug.Log("XR started.");
        }
    }

    private void StopXR()
    {
        Debug.Log("Stopping XR...");
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        Debug.Log("XR stopped.");

        Debug.Log("Deinitializing XR...");
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("XR deinitialized.");

        _mainCamera.ResetAspect();
        _mainCamera.fieldOfView = _defaultFieldOfView;
        _mainCamera.transform.rotation = DEFAULT_CAMERA;

    }
}