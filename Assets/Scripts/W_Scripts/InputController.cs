using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public bool _inputTouchscreenIsActived;
    public bool _inputGamepadIsActived;
    public bool _inputHybridIsActived;
    public GameObject _joysStickPad;

    [SerializeField] InputAction _moveInput = null;
    [SerializeField] InputAction _cameraInput = null;
    private void Update()
    {
        
    }
    void OnEnable()
    {
        _moveInput.Enable();
        _cameraInput.Enable();
    }

    private void OnDisable()
    {
        _moveInput.Disable();
        _cameraInput.Disable();
    }

    public Vector2 CameraInput() 
    {
        
        return _cameraInput.ReadValue<Vector2>();
    }

    public Vector2 MoveInput() 
    {
        return _moveInput.ReadValue<Vector2>();
    }

    public void ChangedModeController(int value) 
    {
        //Value 0 = Touchscreen mode controller
        //Value 1 = Gamepad mode controller
        //Value 2 = Hibryd mode controller

        if (value == 0) 
        {
            _inputTouchscreenIsActived = true;
            _inputHybridIsActived = false;
            _inputGamepadIsActived = false;

            Debug.Log("Entrando a modo screen");
        }
        else

        if(value == 1) 
        {
            _inputTouchscreenIsActived = false;
            _inputHybridIsActived = false;
            _inputGamepadIsActived = true;
            Debug.Log("Entrando a modo gamepad");
        }
        else

        if (value == 2)
        {
            _inputTouchscreenIsActived = false;
            _inputHybridIsActived = true;
            _inputGamepadIsActived = false;
            Debug.Log("Entrando a modo hibryd");
        }
    }
}
