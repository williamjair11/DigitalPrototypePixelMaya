using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
public enum ControlType { Gamepad, Vr, PC, Touch}
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
    [SerializeField] private int _targetFrameRate;
    [SerializeField] UnityEvent _onSetVRMode, _onSetGamepadMode, _onSetTouchMode, _onSetPcMode;

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
        if(controlType != ControlType.Vr && _currentControlType == ControlType.Vr) 
        vrModeController.ExitVR();
        if(controlType != ControlType.Touch)
        uIController.SetActiveTouchControls = false;

        switch (controlType)
        {
            case ControlType.Gamepad:
                _currentControlType =  ControlType.Gamepad;
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
                break;
            default:
                break;
        }
        
    }
}
