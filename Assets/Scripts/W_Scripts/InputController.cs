using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [Header("Status controllers")]
    private bool _inputTouchscreenIsActived = true;
    private bool _inputGamepadIsActived = false;
    private bool _inputHybridIsActived = false;

    [Header("References Objects")]
    public GameObject _joysStickPad;
    private HudController _hudController;

    [Header("InputAction")]
    [SerializeField] InputAction _moveInput = null;
    [SerializeField] InputAction _cameraInput = null;
    [SerializeField] InputAction _jump = null;

    private void Start()
    {
        _hudController = FindObjectOfType<HudController>();
    }
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
        //_joysStickPad.SetActive(true);

        Debug.Log("Entrando a modo screen");
    }

    public void GamepadIsActived() 
    {
        _inputGamepadIsActived = true;
        _inputHybridIsActived = false;
        _inputTouchscreenIsActived = false;

        _joysStickPad.SetActive(false);
        _hudController.HideHud();

        Debug.Log("Entrando a modo gamepad");
    }

    public void VrIsActived() 
    {
        _inputHybridIsActived = true;
     
        _joysStickPad.SetActive(false);

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
