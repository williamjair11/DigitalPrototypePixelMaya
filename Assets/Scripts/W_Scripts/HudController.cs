using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudController : MonoBehaviour
{
    [SerializeField]
    private GameObject _hudCanvas;
    public bool _hudIsActivated;
 
    public void showHud() 
    {
        _hudCanvas.SetActive(true);
        _hudIsActivated = true;
    }

    public void HideHud() 
    {
        _hudCanvas.SetActive(false);
        _hudIsActivated = false;
    }
}
