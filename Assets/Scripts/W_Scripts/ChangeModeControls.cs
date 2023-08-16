using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeModeControls : MonoBehaviour
{
   /* public enum ModeControlType {Gamepad, Touch, Vr}
    [SerializeField] public ModeControlType _modeControlType;

    [SerializeField] private TMP_Dropdown _dropdownModeControl;

    [NonSerialized] public bool _GamepadModeIsActivated= false;
    [NonSerialized] public bool _TouchModeIsActivated = false;
    [NonSerialized] public bool _VrModeIsActivated = false;

    private TouchControlsController _touchControlController;
    private HudController _hudController;
    private PauseController _pauseController;
    private VrModeController _vrModeController;
    private CameraController _cameraController;
    private PlayerController _playerController;


    void Start()
    {
        _touchControlController = FindObjectOfType<TouchControlsController>();
        _hudController = FindObjectOfType<HudController>();
        _pauseController = FindObjectOfType<PauseController>();
        _vrModeController = FindObjectOfType<VrModeController>();
        _cameraController = FindObjectOfType<CameraController>();
        _playerController = FindObjectOfType<PlayerController>();

        _modeControlType = ModeControlType.Touch;       
    }


    void ActivateModeGamepad() 
    {
        if (!_GamepadModeIsActivated) 
        {
            //Rutine Activated ModeGamepad
            _touchControlController.HideTouchControls();
            _hudController.showHud();
            _GamepadModeIsActivated = true;
            _TouchModeIsActivated = false;
            _cameraController.isActivated = true;
        }                            
    }

    void ActivateModeTouch()
    {
        if (!_TouchModeIsActivated) 
        {
            //Rutine Activated ModeTouch
            _touchControlController.showTouchControls();
            _hudController.showHud();
            _TouchModeIsActivated = true;
            _GamepadModeIsActivated = false;
            _cameraController.isActivated = true;            

        }       
    }

    void ActivateModeVr()
    {
        if (!_VrModeIsActivated)
        {
            //Rutine Activated VrMode

            _pauseController.RutineDesactivatePause();
            _touchControlController.HideTouchControls();
            _hudController.HideHud();
            _VrModeIsActivated = true;
            _TouchModeIsActivated = false;
            _GamepadModeIsActivated = false;
            _vrModeController.EnterVR();
            _cameraController.isActivated = false;
            
        }
    }

    public void ChangedModeControlDropdown(int index) 
    {
        // 0 Touchscreen Mode
        // 1 Gamepad Mode
        // 2 Vr Mode

        if (index == 0) 
        { 
            _modeControlType = ModeControlType.Touch;
            _dropdownModeControl.value = 0;
        }
        if (index == 1) 
        { 
            _modeControlType = ModeControlType.Gamepad;
            _dropdownModeControl.value = 1;
        }
        if (index == 2) 
        { 
            _modeControlType = ModeControlType.Vr;
            _dropdownModeControl.value = 2;
        }
    }

    public void DesactivateVr() 
    {               
        _VrModeIsActivated = false;
        _cameraController.isActivated = true;
    }*/
}
