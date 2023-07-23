using UnityEngine;

public abstract class StateMachineCtx : MonoBehaviour
{
    [SerializeField] protected SF _stateFactory;
    [SerializeField] protected BS _currentState;
    public BS CurrentState{get => _currentState; set => _currentState = value;}
    
    public abstract void InitializeStateMachine();
}
