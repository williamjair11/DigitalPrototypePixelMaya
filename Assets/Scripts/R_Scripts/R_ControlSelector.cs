using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class R_ControlSelector : MonoBehaviour
{
    public GameObject _PauseCanvas;
    bool _menuIsActivated = false;
    InputController inputController = new InputController();

    private void Awake()
    {
        
    }

    public void HandleInputData(int val)
    {
        if (val == 0)
        {
            inputController.ChangedModeController(value: 0);
            
        }
        else if (val == 1)
        {
            inputController.ChangedModeController(value: 1);
        }
        else if (val == 2)
        {
            inputController.ChangedModeController(value: 2);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _menuIsActivated == false)
        {
            ShowPause();
            _menuIsActivated = true;

        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _menuIsActivated == true)
        {
            QuitPause();
            _menuIsActivated = false;
        }
    }
    public void ShowPause()
    {
        _PauseCanvas.SetActive(true);
    }
    public void QuitPause()
    {
        _PauseCanvas.SetActive(false);
    }
}

