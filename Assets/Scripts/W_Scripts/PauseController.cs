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
}


