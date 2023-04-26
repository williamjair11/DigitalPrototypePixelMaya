using JetBrains.Annotations;
using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [NonSerialized]
    public bool _inputTouchscreenIsActived = false;
    [NonSerialized]
    public bool _inputGamepadIsActived = false;
    [NonSerialized]
    public bool _inputHybridIsActived = true;

    public GameObject _joysStickPad;

    [SerializeField] InputAction _moveInput = null;
    [SerializeField] InputAction _cameraInput = null;
    [SerializeField] InputAction _jump = null;
    [SerializeField] private TMP_Dropdown _controlSlider;

    void OnEnable()
    {
        _moveInput.Enable();
        _cameraInput.Enable();
        _jump.Enable();
    }

    private void OnDisable()
    {
        _moveInput.Disable();
        _cameraInput.Disable();
        _jump.Disable();
    }

    public Vector2 CameraInput() 
    {     
        return _cameraInput.ReadValue<Vector2>();
    }

    public Vector2 MoveInput() 
    {
        return _moveInput.ReadValue<Vector2>();
    }

    public bool Jump() 
    {
        return true;
    }

    public void ChangedModeController(int value) 
    {       
        if (value == 0) 
        {
            _controlSlider.value = 0;
            TouchScreenIsActived();
        }
        else

        if(value == 1) 
        {
            _controlSlider.value = 1;
            GamepadIsActived();    
        }
        if (value == 2) 
        {
            _controlSlider.value = 2;
            VrIsActived();
        }
    }

    public void TouchScreenIsActived() 
    {
        _inputTouchscreenIsActived = true;
        _inputHybridIsActived = false;
        _inputGamepadIsActived = false;

        Debug.Log("Entrando a modo screen");
    }

    public void GamepadIsActived() 
    {
        _inputGamepadIsActived = true;
        _inputTouchscreenIsActived = false;
        _inputHybridIsActived=false;
        
        _joysStickPad.SetActive(false);

        Debug.Log("Entrando a modo gamepad");
    }

    public void VrIsActived() 
    {
        _inputHybridIsActived = true;
        _inputTouchscreenIsActived=false;
        _inputGamepadIsActived=false;
        
        Debug.Log("Entrando a modo hibryd");
    }

    public void ShowJoystick() 
    {
        _joysStickPad.SetActive(true);
    }

    public void HideJoystick()
    {
        _joysStickPad.SetActive(false);
    }
}
