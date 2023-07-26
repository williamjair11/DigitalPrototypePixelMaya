using UnityEngine;

public class TouchControlsController : MonoBehaviour
{
    [SerializeField] private GameObject _touchControlsCanvas;

    private PauseController _pauseController;

    public bool _touchControlsIsActivated;

    private void Start()
    {
        _pauseController = FindObjectOfType<PauseController>();
    }
    public void showTouchControls()
    {
        if (!_touchControlsIsActivated && _pauseController._pauseIsActivated == false) 
        {
            _touchControlsCanvas.SetActive(true);
            _touchControlsIsActivated = true;
        }        
    }

    public void HideTouchControls()
    {
        if (_touchControlsIsActivated) 
        {
            _touchControlsCanvas.SetActive(false);
            _touchControlsIsActivated = false;
        }
    } 
}
