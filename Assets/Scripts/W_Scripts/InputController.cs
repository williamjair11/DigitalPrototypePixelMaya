using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [Header("InputAction")]
    [SerializeField] InputAction _moveInput = null;
    [SerializeField] InputAction _cameraInput = null;
    [SerializeField] InputAction _jump = null;
    [SerializeField] InputAction _throwBallEnergy = null;
    [SerializeField] InputAction _flashHability = null;
    [SerializeField] public InputAction _interact = null;
    [SerializeField] InputAction _twrowObject = null;
    [SerializeField] InputAction _runPlayer = null;

    void OnEnable()
    {
        _moveInput.Enable();
        _cameraInput.Enable();
        _jump.Enable();
        _flashHability.Enable();
        _throwBallEnergy.Enable();
        _interact.Enable();
        _twrowObject.Enable();
        _runPlayer.Enable();
    }

    private void OnDisable()
    {
        _moveInput.Disable();
        _cameraInput.Disable();
        _jump.Disable();
        _flashHability.Disable();
        _throwBallEnergy.Disable();
        _interact.Disable();
        _twrowObject.Disable();
        _runPlayer.Disable();
    }

    private void Update()
    {
        ThrowBallEnergy();
        FlashHability();
        Interact();
        ThrowObject();
        
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
        bool state = false;
        if (_jump.WasPressedThisFrame()) 
        {
            state = true;
        }
        return state;
    }

    public bool Interact() 
    {
        bool state = false;
        if (_interact.WasPressedThisFrame())
        {
            state = true;
        }
        return state;
    }

    public bool ThrowObject()
    {
        bool state = false;
        if (_twrowObject.WasPressedThisFrame())
        {
            state = true;
        }
        return state;
    }
    public bool FlashHability() 
    {

        bool state = false;
        if (_flashHability.WasPressedThisFrame())
        {
            state = true;            
        }
        return state;
    }

    public bool RunPlayer()
    {
        bool state = false;
        if (_runPlayer.IsPressed())
        {
            state = true;
        }
        return state;
    }

    public bool ThrowBallEnergy()
    {
        bool state = false;
        if (_throwBallEnergy.WasPressedThisFrame()) 
        {
            state = true;
        }
        return state;
    }
}
