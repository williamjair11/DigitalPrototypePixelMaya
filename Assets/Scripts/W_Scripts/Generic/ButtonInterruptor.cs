using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ButtonInterruptor : Interruptor
{
    // Start is called before the first frame update
    void OnTriggerStay(Collider other)
    {
        if(_tags.Contains(other.tag))
        {
            _isActive = true;
            _onChange?.Invoke();
            _onActive?.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(_tags.Contains(other.tag))
        {
            _isActive = false;
           _onChange?.Invoke();
            _onDesactive?.Invoke();
        }
    }
}
