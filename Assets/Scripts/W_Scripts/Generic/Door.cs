using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using DG.Tweening;
using System;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject _rigthDoor, _leftDoor; 
    [SerializeField] private UnityEvent _onOpenDoor, _onCloseDoor;
    [SerializeField] private List<Interruptor> _interruptors = new List<Interruptor>();
    
    [SerializeField] private bool _isOpen;

    public void CheckDoorState()
    {
        Debug.Log("_interruptors[0]");
        Debug.Log(_interruptors[0].IsActive);
        Debug.Log(_interruptors.Count);
        bool interruptorsAreActive = true;
        foreach (var Interruptor in _interruptors)
        {
            
            
            if(!Interruptor.IsActive)
            { 
                interruptorsAreActive = false; break; 
                
            }
        }
        if(interruptorsAreActive && !_isOpen) OpenDoor();
        if(!interruptorsAreActive && _isOpen) CloseDoor();
    }

    public void OpenDoor()
    {
        Debug.Log("ShouldOpen");
        //_rigthDoor.transform.Rotate(Vector3.forward, -90, Space.Self);
        _rigthDoor.transform.DORotate(new Vector3(-90, 90, 0), 2);
        //_leftDoor.transform.Rotate(Vector3.forward, 90, Space.Self);
        _leftDoor.transform.DORotate(new Vector3(-90, 90, 0), 2);
        _isOpen = true;
        _onOpenDoor?.Invoke();
    }

    public void CloseDoor()
    {
        Debug.Log("ShouldClose");
        //_rigthDoor.transform.Rotate(Vector3.forward, 90, Space.Self);
        _rigthDoor.transform.DORotate(new Vector3(-90, 0, 0), 2);
        //_leftDoor.transform.Rotate(Vector3.forward, -90, Space.Self);
        _leftDoor.transform.DORotate(new Vector3(-90, -180, 0), 2);
        _isOpen = false;
        _onCloseDoor?.Invoke();
    }

   /* void RotateDoor(float direction, Transform doorTransform)
    {
        Quaternion targetRotation = Quaternion.Euler(-90, 0, 90 * direction);
        doorTransform.rotation = Quaternion.Lerp(doorTransform.localRotation, targetRotation, Time.deltaTime * 2);
    } */
}
