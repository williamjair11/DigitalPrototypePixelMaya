using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class R_ControlSelector : MonoBehaviour
{
    public GameObject _PauseCanvas;
    bool _menuIsActivated = false;

    private void Awake()
    {
        
    }

    public void HandleInputData(int val)
    {
        if (val == 0)
        {
            Debug.Log("Touch control enabled");
        }
        else if (val == 1)
        {
            Debug.Log("Wireless control enabled");
        }
        else if (val == 2)
        {
            Debug.Log("Hybrid control enabled");
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _menuIsActivated == false)
        {
            ShowPause();
            _menuIsActivated = true;

        }
        else if (Input.GetKeyDown(KeyCode.Space) && _menuIsActivated == true)
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

