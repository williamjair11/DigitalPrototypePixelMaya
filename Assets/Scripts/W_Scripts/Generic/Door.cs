using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject _rigthDoor, _leftDoor; 
    [SerializeField] private UnityEvent _onOpenDoor, _onCloseDoor;
    [SerializeField] private List<Interruptor> _interruptors = new List<Interruptor>();
    private bool _isOpen;

    public void CheckDoorState()
    {
        bool interruptorsAreActive = true;
        foreach (var Interruptor in _interruptors)
        {
            
            if(!Interruptor.IsActive) { interruptorsAreActive = false; break; }
        }
        if(interruptorsAreActive && !_isOpen) OpenDoor();
        if(!interruptorsAreActive && _isOpen) CloseDoor();
    }

    public void OpenDoor()
    {
        _rigthDoor.transform.Rotate(Vector3.forward, -90, Space.Self);
        _leftDoor.transform.Rotate(Vector3.forward, 90, Space.Self);
        _isOpen = true;
        _onOpenDoor?.Invoke();
    }

    public void CloseDoor()
    {
        _rigthDoor.transform.Rotate(Vector3.forward, 90, Space.Self);
        _leftDoor.transform.Rotate(Vector3.forward, -90, Space.Self);
        _isOpen = false;
        _onCloseDoor?.Invoke();
    }

    void RotateDoor(float direction, Transform doorTransform)
    {
        Quaternion targetRotation = Quaternion.Euler(-90, 0, 90 * direction);
        doorTransform.rotation = Quaternion.Lerp(doorTransform.localRotation, targetRotation, Time.deltaTime * 2);
    }
}
