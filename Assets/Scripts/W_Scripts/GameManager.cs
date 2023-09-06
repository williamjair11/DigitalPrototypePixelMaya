using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using DG.Tweening;

public enum ControlType { Gamepad, Vr, PC, Touch}
public enum GamepadType {dualShok, XInput, None}
public enum GameState {InGame, Pause, GameOver}
public class GameManager : MonoBehaviour
{
    //Todo: Para el código del juego (ya no prototipo) manejar cambio de controlType con Actions o con el patron observer en lugar de Unity Events
    public static GameManager Instance = null;
    public float _defaultFieldOfView;
    public ControlType CurrentControlType { get => _currentControlType; }
    public PlayerInventory playerInventory;
    public UIController uIController;
    public PlayerController playerController;
    public VrModeController vrModeController;
    private GameState _currentGameState = GameState.InGame;
    [SerializeField] ResizeCanvas _resizeCanvas;
    [SerializeField] private int _targetFrameRate;
    [SerializeField] UnityEvent _onSetVRMode, _onSetGamepadMode, _onSetTouchMode, _onSetPcMode;
    [SerializeField] GamepadType _currentGamepadType = GamepadType.None;
    [SerializeField] ControlType _currentControlType = ControlType.Gamepad, _lastControlType = ControlType.Gamepad;
    public GameState CurrentGameState{get => _currentGameState;}
    public GamepadType CurrentGamepadType {get => _currentGamepadType;}
    void Awake()
    {
        DOTween.Init();
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

    void Start()
    {
        #if UNITY_ANDROID
            SetControlMode(ControlType.Touch); 
        #endif
    }

    void Update()
    {
        OnExitVRMode();
    }

    public void OnExitVRMode()
    {
        if(Camera.main.fieldOfView != _defaultFieldOfView && _currentControlType == ControlType.Vr)
        {
            SetControlMode(_lastControlType);
        }
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
        else if (gamepad is UnityEngine.InputSystem.Gamepad)
        {
            _currentGamepadType = GamepadType.XInput;
            Debug.Log("Se ha conectado un control de Xbox.");
        }
    }

    public void SetGamepadControlMode()
    {
        CheckGamepadType();
        if(_currentGamepadType == GamepadType.dualShok || _currentGamepadType == GamepadType.XInput) 
            SetControlMode(ControlType.Gamepad);
    }

    public void SetVRMode()
    {   
        CheckGamepadType();
        if(_currentGamepadType == GamepadType.None) return;
        SetControlMode(ControlType.Vr);
    }
    public void SetMouseAndKeyBoardMode() =>  SetControlMode(ControlType.PC);

    public void SetTouchControlsMode() => SetControlMode(ControlType.Touch);

    public void SetGameState(GameState _state)
    {
        switch (_state)
        {
            case GameState.InGame:
            _currentGameState = GameState.InGame;
            if(_currentControlType == ControlType.Touch)
            uIController.SetActiveTouchControls = true;
            uIController.SetActiveHud = true;
            Time.timeScale = 1;
                break;
            case GameState.Pause:
            playerController.CanJump = false;
            Time.timeScale = 0;
            _currentGameState = GameState.Pause;
            uIController.SetActiveTouchControls = false;
            uIController.SetActiveHud = false;
                break;
            case GameState.GameOver:
            RestartScene();
                break;
            default:
                break;
        }
    }

    public void SetControlMode(ControlType controlType)
    {
        if(_currentControlType == controlType) return;
        if(controlType != ControlType.Touch) uIController.SetActiveTouchControls = false;
        if(controlType != ControlType.Vr && _currentControlType == ControlType.Vr) 
        {
            uIController.SetActiveHud = true;
            _resizeCanvas.SetNormalMode();
            _currentControlType = controlType;
            RestartScene();
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
                _currentGamepadType = GamepadType.None;
                _currentControlType =  ControlType.Touch;
                uIController.SetActiveTouchControls = true;
                uIController.SetInteractionMessageIcon();
                _onSetTouchMode?.Invoke();
                break;
            default:
                break;
        }
        _lastControlType = _currentControlType;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
