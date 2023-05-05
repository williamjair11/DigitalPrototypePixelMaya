using UnityEngine;

public class TouchControlsController : MonoBehaviour
{
    [SerializeField] private GameObject _touchControlsCanvas;

    private bool _touchControlsIsActivated;

    public void showTouchControls()
    {
        _touchControlsCanvas.SetActive(true);
        _touchControlsIsActivated = true;
    }

    public void HideTouchControls()
    {
        _touchControlsCanvas.SetActive(false);
        _touchControlsIsActivated= false;
    }
    
}
