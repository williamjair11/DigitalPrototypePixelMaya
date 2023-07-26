using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseCanvas;

    public bool _pauseIsActivated = false;

    private HudController _hudController;
    private TouchControlsController _touchControlsController;
    private ChangeModeControls _changeModeControls;
    void Start()
    {
        _hudController = FindObjectOfType<HudController>();
        _touchControlsController = FindObjectOfType<TouchControlsController>();
        _changeModeControls = FindObjectOfType<ChangeModeControls>();
        _pauseCanvas.SetActive(false);
    }

    public void RutineActivatePause() 
    {
        if (!_pauseIsActivated) 
        {
            Time.timeScale = 0f;
            _pauseCanvas.SetActive(true);
            _pauseIsActivated = true;
            _touchControlsController.HideTouchControls();
            _hudController.HideHud();           
        }
    }

    public void RutineDesactivatePause() 
    {
        if (_pauseIsActivated) 
        {
            Time.timeScale = 1.0f;
            _pauseCanvas.SetActive(false);
            _pauseIsActivated = false;
            _hudController.showHud();
            if(_changeModeControls._modeControlType == ChangeModeControls.ModeControlType.Touch) 
            {
                _touchControlsController.showTouchControls();
            }
        }      
    }
}


