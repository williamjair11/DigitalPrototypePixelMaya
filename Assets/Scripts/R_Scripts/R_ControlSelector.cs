using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class R_ControlSelector : MonoBehaviour
{
    public GameObject _PauseCanvas;
    bool _menuIsActivated = false;
    InputController _controlSelector = new InputController();


    private void Awake()
    {
        
    }

    public void HandleInputData(int val)
    {
        if (val == 0)
        {
            _controlSelector.ChangedModeController(val);
        }
        else if (val == 1)
        {
            _controlSelector.ChangedModeController(val);
        }
        else if (val == 2)
        {
            _controlSelector.ChangedModeController(val);
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

