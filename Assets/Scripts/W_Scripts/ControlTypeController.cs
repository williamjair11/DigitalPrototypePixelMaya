using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ControlTypeController : MonoBehaviour
{
   public ControlType _currentControlType = ControlType.Gamepad;

   void Awake()
   {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded +=  OnSceneLoaded;
   }

   void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Aquí puedes poner el código que quieres ejecutar cuando se carga una escena
        if(_currentControlType == ControlType.Vr)
        GameManager.Instance.vrModeController.EnterVR();
    }
}
