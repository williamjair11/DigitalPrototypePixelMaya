using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : MonoBehaviour
{
    protected EnemyBaseState _currentState;
    protected Tzitzimime _context;
    
    public abstract void OnStartState();
    public abstract void OnExitState();
    public abstract void fakeUpdate();

    protected void SwitchState(EnemyBaseState newState){
        _currentState = newState;
    }

    // Update is called once per frame
    void Update()
    {
        fakeUpdate();
    }


}
