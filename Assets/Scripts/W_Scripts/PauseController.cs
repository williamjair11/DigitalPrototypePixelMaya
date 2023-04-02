using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public GameObject _pause;
    InputController _inputController;
    public void Start()
    {
        _pause.SetActive(false);
        _inputController = GetComponent<InputController>();
    }

    public void Update()
    {
        if (Keyboard.current[Key.Space].wasPressedThisFrame) 
        {
            _pause.SetActive(true);
        }     
    }

    public void HidePause() 
    {
        _pause.SetActive(false);
    }

    public void ShowPause() 
    {
        _pause.SetActive(true);
    }

    public void HandleInputData(int val)
    {
        if (val == 0)
        {
            _inputController.ChangedModeController(0);
            Debug.Log("TouchScreen control enabled");
        }
        else if (val == 1)
        {
            _inputController.ChangedModeController(1);
            Debug.Log("Wireless control enabled");
        }
        else if (val == 2)
        {
            _inputController.ChangedModeController(2);
            Debug.Log("Hybrid control enabled");
        }
    }

}
