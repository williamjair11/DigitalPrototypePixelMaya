using System.Collections;
using Google.XR.Cardboard;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.Management;
using UnityEngine.Events;

public class VrModeController : MonoBehaviour
{
    [Header("Camera")]
    public Transform _cameraTransform;
    private quaternion DEFAULT_CAMERA;
    public CameraController _cameraController;

    [Header("Selection controls Canvas")]
    [SerializeField] private TMP_Dropdown _dropdown;

    [Header("Events")]
    [SerializeField] private UnityEvent showPauseCanvas;

    public bool _isScreenTouched
    {
        get
        {
            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        }
    }

    public bool _isVrModeEnabled
    {
        get
        {
            return XRGeneralSettings.Instance.Manager.isInitializationComplete;
        }
    }
    public void Start()
    {
        //Save states camera
        _cameraController = new CameraController();
        DEFAULT_CAMERA = _cameraController.DEFAULT_CAMERA;

        //Parameters 
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.brightness = 1.0f;

        if (!Api.HasDeviceParams())
        {
            Api.ScanDeviceParams();
        }
    }
    public void Update()
    {

            if (Api.IsCloseButtonPressed)
            {               
                ExitVR();               
            }

            if (Api.IsGearButtonPressed)
            {
                Api.ScanDeviceParams();
            }
             
            Api.UpdateScreenParams();         

    }

    public void EnterVR()
    {       
            Debug.Log("entrando a modo VR");
            
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

    public IEnumerator StartXR()
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
    public void StopXR()
    {      
        Debug.Log("Stopping XR...");
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        Debug.Log("XR stopped.");

        Debug.Log("Deinitializing XR...");
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("XR deinitialized.");

        _cameraTransform.rotation = DEFAULT_CAMERA;
        showPauseCanvas.Invoke();
        _dropdown.value = 0;
    }
}
