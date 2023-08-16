using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [Header("InputAction")]
    PlayerInput _playerInput;
    [SerializeField] UnityEvent<Vector2> _onMoveEvent, _onRotateCameraEvent;
    [SerializeField] UnityEvent
    _onMoveInputStart,
    _onMoveInputCancel,
    _onPressDownAttackButton, 
    _onPressUpAttackButton, 
    _onPressDownSpecialAttackButton, 
    _onPressUpSpecialAttackButton,
    _onPressDownInteractButton, 
    _onPressUpInteractButton,
    _onPressShorTWeaponsInventoryButton,
    _onPressLongWeaponsInventoryButton,
    _onPressSpecialAttackInventoryButton,
    _onPressUpRunButton,
    _onPressDownRunButton,
    _onPressInventoryButton,
    _onPressUpJumpButton,
    _onPressDownJumpButton,
    _onPressDownCrouchButton,
    _onPressUpCrouchButton,
    _onPressThrowButton,
    _onRotateCameraStart,
    _onRotateCameraCancel,
    _onPressPauseButton;

    void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Gameplay.Move.started += OnMoveStart;
        _playerInput.Gameplay.Move.performed += OnMoveInputChange;
        _playerInput.Gameplay.Move.canceled += OnMoveInputCancel;
        _playerInput.Gameplay.RotateCamera.performed += OnRotateCameraInput;
        _playerInput.Gameplay.RotateCamera.started += OnRotateCameraStart;
        _playerInput.Gameplay.RotateCamera.canceled += OnRotateCameraCancel;
        _playerInput.Gameplay.Attack.started += OnPressDownAttackButton;
        _playerInput.Gameplay.Attack.canceled += OnPressUpAttackButton;
        _playerInput.Gameplay.SpecialAttack.started += OnPressDownSpecialAttackButton;
        _playerInput.Gameplay.SpecialAttack.canceled += OnPressUpSpecialAttackButton;
        _playerInput.Gameplay.Run.started += OnPressDownRunButton;
        _playerInput.Gameplay.Run.canceled += OnPressUpRunButton;
        _playerInput.Gameplay.Interact.started += OnPressDownInteractButton;
        _playerInput.Gameplay.Interact.canceled += OnPressUpInteractButton;
        _playerInput.Gameplay.Jump.started += OnPressDownJumpButton;
        _playerInput.Gameplay.Jump.canceled += OnPressUpJumpButton;
        _playerInput.Gameplay.Inventory.started += OnPressInventoryButton;
        _playerInput.Gameplay.Crouch.started += OnPressDownCrouchButton;
        _playerInput.Gameplay.Crouch.canceled += OnPressUpCrouchButton;
        _playerInput.Gameplay.Pause.started += OnPressPauseButton;
    }

    void OnMoveInputChange(InputAction.CallbackContext ctx) { _onMoveEvent?.Invoke(ctx.ReadValue<Vector2>());}
    void OnMoveStart(InputAction.CallbackContext ctx) { _onMoveInputStart?.Invoke();}
    void OnMoveInputCancel(InputAction.CallbackContext ctx) { _onMoveInputCancel?.Invoke();}
    void OnRotateCameraInput(InputAction.CallbackContext ctx) => _onRotateCameraEvent?.Invoke(ctx.ReadValue<Vector2>());
    void OnRotateCameraStart(InputAction.CallbackContext ctx) => _onRotateCameraStart?.Invoke();
    void OnRotateCameraCancel(InputAction.CallbackContext ctx) => _onRotateCameraCancel?.Invoke();
    void OnPressDownAttackButton(InputAction.CallbackContext ctx) => _onPressDownAttackButton?.Invoke();
    void OnPressUpAttackButton(InputAction.CallbackContext ctx) => _onPressUpAttackButton?.Invoke();
    void OnPressDownSpecialAttackButton(InputAction.CallbackContext ctx) => _onPressDownSpecialAttackButton?.Invoke();
    void OnPressUpSpecialAttackButton(InputAction.CallbackContext ctx) => _onPressUpSpecialAttackButton?.Invoke();
    void OnPressDownInteractButton(InputAction.CallbackContext ctx) => _onPressDownInteractButton?.Invoke();
    void OnPressUpInteractButton(InputAction.CallbackContext ctx) => _onPressUpInteractButton?.Invoke();
    void OnPressShorWeaponsInventoryButton(InputAction.CallbackContext ctx) => _onPressUpInteractButton?.Invoke();
    void OnPressDownRunButton(InputAction.CallbackContext ctx) => _onPressDownRunButton?.Invoke();
    void OnPressUpRunButton(InputAction.CallbackContext ctx) => _onPressUpRunButton?.Invoke();
    void OnPressDownJumpButton(InputAction.CallbackContext ctx) => _onPressDownJumpButton?.Invoke();
    void OnPressUpJumpButton(InputAction.CallbackContext ctx) => _onPressUpJumpButton?.Invoke();    
    void OnPressInventoryButton(InputAction.CallbackContext ctx) => _onPressInventoryButton?.Invoke();
    void OnPressDownCrouchButton(InputAction.CallbackContext ctx) => _onPressDownCrouchButton?.Invoke();
    void OnPressUpCrouchButton(InputAction.CallbackContext ctx) => _onPressUpCrouchButton?.Invoke();
    void OnPressPauseButton(InputAction.CallbackContext ctx) => _onPressPauseButton?.Invoke();
    
    
    void OnEnable()
    {
        _playerInput.Enable();
      
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Update()
    {
        
    }

}
