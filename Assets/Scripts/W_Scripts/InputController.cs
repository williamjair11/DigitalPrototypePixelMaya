using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [Header("Status controllers")]
    public bool _inputTouchscreenIsActived = false;
    public bool _inputGamepadIsActived = false;
    public bool _inputHybridIsActived = true;

    [Header("InputAction")]
    [SerializeField] InputAction _moveInput = null;
    [SerializeField] InputAction _cameraInput = null;
    [SerializeField] InputAction _jump = null;

    [Header("Events")]
    [SerializeField] private UnityEvent showPauseCanvas;
    [SerializeField] private UnityEvent hidePauseCanvas;

    [SerializeField] private UnityEvent showHudCanvas;
    [SerializeField] private UnityEvent hideHudCanvas;

    [SerializeField] private UnityEvent showTouchControlsCanvas;
    [SerializeField] private UnityEvent hideTouchControlsCanvas;

    [SerializeField] private UnityEvent activatedApiVR;

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
        //Value 0 = Touchscreen mode controller
        //Value 1 = Gamepad mode controller      
        
        if (value == 0) 
        {
            TouchScreenIsActived();
        }
        else

        if(value == 1) 
        {
            GamepadIsActived();    
        }
        if(value == 2) 
        {
            VrIsActived();
        }
    }

    public void TouchScreenIsActived() 
    {
        _inputTouchscreenIsActived = true;
        _inputHybridIsActived = false;
        _inputGamepadIsActived = false;

        showHudCanvas.Invoke();
        Debug.Log("Entrando a modo screen");
    }

    public void GamepadIsActived() 
    {
        _inputGamepadIsActived = true;
        _inputHybridIsActived = false;
        _inputTouchscreenIsActived = false;

        hideTouchControlsCanvas.Invoke();
        showHudCanvas.Invoke();
        Debug.Log("Entrando a modo gamepad");
    }

    public void VrIsActived() 
    {
        _inputHybridIsActived = true;
        _inputTouchscreenIsActived = false;
        _inputGamepadIsActived = false;

        hideHudCanvas.Invoke();
        hidePauseCanvas.Invoke();
        hideTouchControlsCanvas.Invoke();
        activatedApiVR.Invoke();
        Debug.Log("Entrando a modo hibryd");
    }
}
