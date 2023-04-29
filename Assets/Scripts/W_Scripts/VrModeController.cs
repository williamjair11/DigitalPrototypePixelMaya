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
    [Header("References scripts objects")]
    private HudController _hudController;
    private PauseController _pauseController;


    [Header("Camera")]
    public Transform _cameraTransform;
    private quaternion DEFAULT_CAMERA;
    public CameraController _cameraController;

    public GameObject _joystickCanvas;

    [Header("Selection controls Canvas")]
    [SerializeField]
    private TMP_Dropdown _dropdown;


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
        //References scripts
        _hudController = FindObjectOfType<HudController>();
        _pauseController = FindObjectOfType<PauseController>();

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
        #if !UNITY_EDITOR
        if (_isVrModeEnabled)
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
        else
        {
            // TODO(b/171727815): Add a button to switch to VR mode.
            //if (_isScreenTouched)
            //{
            //    EnterVR();
            //}
        }
        #endif
    }

    public void EnterVR(int val)
    {
        _hudController.HideHud();
        _pauseController.HidePause();
        if (val == 2) 
        {
            Debug.Log("entrando a modo VR");
            
            StartCoroutine(StartXR());           
            if (Api.HasNewDeviceParams())
            {
                Api.ReloadDeviceParams();
            }
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
        _dropdown.value = 0;
        _joystickCanvas.SetActive(true);
        _hudController.showHud();
    }
}
