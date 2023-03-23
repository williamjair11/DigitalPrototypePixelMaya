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
    void Start()
    {
        _pause.SetActive(false);
        _inputController = GetComponent<InputController>();
    }

    // Update is called once per frame
    void Update()
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

    public void GetIndex(int value) 
    {
        if(value == 0) { _inputController.ChangedModeController(0); }
    }

}
