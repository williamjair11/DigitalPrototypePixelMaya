using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject _pause;
    private bool _pauseIsActivated;
    InputController _inputController;
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private GameObject _joystickCanvas;
    [SerializeField] private GameObject _hudCanvas;
    private HudController _hudController;
    public void Start()
    {
        _hudController = FindObjectOfType<HudController>();
        _pause.SetActive(false);
        _inputController = GetComponent<InputController>();
    }

    public void HidePause() 
    {
        _pause.SetActive(false);
        _pauseIsActivated = false;
        if (_dropdown.value == 0) 
        {
            _joystickCanvas.SetActive(true);
        }
        //_hudCanvas.SetActive(true);
        _hudController.showHud();
    }

    public void ShowPause() 
    {
        _pause.SetActive(true);
        _pauseIsActivated = true;
        _joystickCanvas.SetActive(false);
        //_hudCanvas.SetActive(false);
        _hudController.HideHud();
    }   
}


