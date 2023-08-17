using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ResizeCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    // Start is called before the first frame update
    public RectTransform canvasTransform;
    private float  _lastCameraAspect, _lastScreenWidth, _lastScreenHeigth;
    public float _canvasHeigth, _canvasVRheigth, _canvasZDistance = .3f, _initialFOV;

   /* void Awake()
    {
        float canvasHeight = canvasTransform.rect.height;
        float desiredCanvasWidth = canvasHeight * Camera.main.aspect;
        canvasTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, desiredCanvasWidth);
    }

    void Update () {
    transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;
    } */


    void Start()
    {
        SetNormalMode();
    }

    
    public void SetVRMode()
    {
        CalculateCanvasSize(_canvasVRheigth);
    }

    public void SetNormalMode()
    {
        CalculateCanvasSize(_canvasHeigth);
    }

    void CalculateCanvasSize(float height)
    {
        canvasTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, height);
        float canvasWidth = Screen.width * height / Screen.height;
        canvasTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, canvasWidth);
    }


}
