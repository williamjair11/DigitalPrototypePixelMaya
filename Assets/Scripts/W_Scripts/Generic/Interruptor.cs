using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
public class Interruptor : MonoBehaviour
{
    [SerializeField] protected List<string> _tags;
    [SerializeField] protected bool _isActive = false;
    [SerializeField] public UnityEvent _onChange, _onActive, _onDesactive;
    public bool IsActive{get => _isActive; set => _isActive = value;}
}
