using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineContext : MonoBehaviour
{
    [SerializeField] protected StatesFactory _stateFactory;
    [SerializeField] protected BaseState _currentState;
    public BaseState CurrentState{get => _currentState; set => _currentState = value;}
    
    public abstract void InitializeStateMachine();
    
}
