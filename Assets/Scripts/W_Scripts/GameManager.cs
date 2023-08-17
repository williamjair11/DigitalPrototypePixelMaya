using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public enum ControlType { Gamepad, Vr, PC, Touch}
public enum GamepadType {dualShok, XInput, None}
public class GameManager : MonoBehaviour
{
    //Todo: Para el código del juego (ya no prototipo) manejar cambio de controlType con Actions o con el patron observer en lugar de Unity Events
    public static GameManager Instance = null;
    ControlType _currentControlType = ControlType.Gamepad;
    public ControlType CurrentControlType { get => _currentControlType; }
    public PlayerInventory playerInventory;
    public UIController uIController;
    public PlayerController playerController;
    public VrModeController vrModeController;
    [SerializeField] ResizeCanvas _resizeCanvas;
    [SerializeField] private int _targetFrameRate;
    [SerializeField] UnityEvent _onSetVRMode, _onSetGamepadMode, _onSetTouchMode, _onSetPcMode;
    GamepadType _currentGamepadType = GamepadType.None;

    public GamepadType CurrentGamepadType
    {get => _currentGamepadType;}
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        Application.targetFrameRate = _targetFrameRate;
    }

    public void CheckGamepadType()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            _currentGamepadType = GamepadType.None;
            Debug.Log("No hay ningún control conectado.");
            return;
        }

        if (gamepad is UnityEngine.InputSystem.DualShock.DualShockGamepad)
        {
            _currentGamepadType = GamepadType.dualShok;
            Debug.Log("Se ha conectado un control de PlayStation.");
        }
        else if (gamepad is UnityEngine.InputSystem.XInput.XInputController)
        {
            _currentGamepadType = GamepadType.XInput;
            Debug.Log("Se ha conectado un control de Xbox.");
        }
    }

    void Start()
    {
        #if UNITY_ANDROID
            SetControlMode(ControlType.Touch); 
        #endif
        //Todo: configurar el modo de control segun la plataforma
       /* #if !UNITY_EDITOR
        SetGamepadControlMode();
        #else
        SetTouchControlsMode();
        #endif */
    }

    public void SetVRMode() => SetControlMode(ControlType.Vr);
    public void SetMouseAndKeyBoardMode() =>  SetControlMode(ControlType.PC);
    public void SetGamepadControlMode() =>  SetControlMode(ControlType.Gamepad);
    public void SetTouchControlsMode() => SetControlMode(ControlType.Touch);

    public void SetControlMode(ControlType controlType)
    {
        if(controlType != ControlType.Touch)
        uIController.SetActiveTouchControls = false;
        if(controlType != ControlType.Vr && _currentControlType == ControlType.Vr) 
        {
            uIController.SetActiveTouchControls = true;
            uIController.SetActiveHud = true;
            _resizeCanvas.SetNormalMode();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //vrModeController.ExitVR();
        }

        switch (controlType)
        {
            case ControlType.Gamepad:
                _currentControlType =  ControlType.Gamepad;
                uIController.SetInteractionMessageIcon();
                break;
            case ControlType.PC:
                _currentControlType =  ControlType.PC;
                break;
            case ControlType.Vr:
                _currentControlType =  ControlType.Vr;
                vrModeController.EnterVR();
                break;
            case ControlType.Touch:
                _currentControlType =  ControlType.Touch;
                uIController.SetActiveTouchControls = true;
                uIController.SetInteractionMessageIcon();
                break;
            default:
                break;
        }
        
    }
}
