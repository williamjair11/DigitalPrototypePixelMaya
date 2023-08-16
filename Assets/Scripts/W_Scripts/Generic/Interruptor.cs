using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class Interruptor : MonoBehaviour
{
    [SerializeField] List<string> _tags;
    private bool _isActive = false;
    [SerializeField] UnityEvent _onChange;
    public bool IsActive{get => _isActive;}

    

    void OnTriggerStay(Collider other)
    {
        
        if(_tags.Contains(other.tag))
        {
            _isActive = true;
            _onChange?.Invoke();
        }
        else
        {
            _isActive = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(_tags.Contains(other.tag))
        {
            _isActive = false;
           _onChange?.Invoke();
            
        }
    }
    
}
