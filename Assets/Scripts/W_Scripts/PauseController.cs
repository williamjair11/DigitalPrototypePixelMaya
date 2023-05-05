using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    private bool _pauseIsActivated;

    [SerializeField] private GameObject _pause;
    
    [SerializeField] private TMP_Dropdown _dropdown;

    [SerializeField] private UnityEvent showHudCanvas;
    [SerializeField] private UnityEvent hideHudCanvas;

    [SerializeField] private UnityEvent showTouchControlsCanvas;
    [SerializeField] private UnityEvent hideTouchControlsCanvas;

    [SerializeField] private InputController _touchControlsIsActivated;

    private bool activatedControls;

    void Update()
    {

    }
    public void HidePause()
    {
        if (_pauseIsActivated)
        {
            _pause.SetActive(false);
            _pauseIsActivated = false;
            if (_dropdown.value == 0)
            {
                showTouchControlsCanvas.Invoke();
                showHudCanvas.Invoke();
            }
            if (_dropdown.value == 1) 
            {
                showHudCanvas.Invoke();
            }
        }            
    }

    public void ShowPause() 
    {
        if (_pauseIsActivated == false) 
        {
            _pause.SetActive(true);
            hideTouchControlsCanvas.Invoke();
            hideHudCanvas.Invoke();
            _pauseIsActivated = true;
        }   
    }   
}


