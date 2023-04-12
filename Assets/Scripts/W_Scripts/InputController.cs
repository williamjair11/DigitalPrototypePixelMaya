using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public bool _inputTouchscreenIsActived = true;
    public bool _inputGamepadIsActived = false;
    public bool _inputHybridIsActived = false;

    public GameObject _joysStickPad;
    VrModeController _vrModeController;
   

    [SerializeField] InputAction _moveInput = null;
    [SerializeField] InputAction _cameraInput = null;


    private void Start()
    {
        _vrModeController = new VrModeController();     
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
        
        if (value == 0) 
        {
            TouchScreenIsActived();
        }
        else

        if(value == 1) 
        {
            GamepadIsActived();    
        }       
    }

    public void TouchScreenIsActived() 
    {
        _inputTouchscreenIsActived = true;
      
        //_joysStickPad.SetActive(true);

        Debug.Log("Entrando a modo screen");
    }

    public void GamepadIsActived() 
    {
        _inputGamepadIsActived = true;

        _joysStickPad.SetActive(false);

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
