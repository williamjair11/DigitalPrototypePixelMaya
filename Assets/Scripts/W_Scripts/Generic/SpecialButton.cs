using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

public class SpecialButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] UnityEvent OnButonDownEvent, OnButtonUpEvent;
    private bool _holdingButton = false;
    public bool HoldingButton{get => _holdingButton;}
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("HoldingButton");
        _holdingButton = true;
        OnButonDownEvent?.Invoke();
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        OnButtonUpEvent?.Invoke();
        _holdingButton = false;
    }

}
