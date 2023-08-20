using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatesFactory
{
    protected StateMachineContext _context;
    protected string _defaultStateId;
    protected Dictionary<string, BaseState> _statesList = new Dictionary<string, BaseState>();
    public string DefaultStateId{set => _defaultStateId = value;}
    public StatesFactory(StateMachineContext currentContext)
    {
        
    }
    //En este método se deben agregar las instancias de los distintos states del personaje
    public abstract void SetUpStatesList();
    public BaseState GetState(string stateId)
    {   
        BaseState newState = _statesList[_defaultStateId];
        if( _statesList.ContainsKey(stateId))
        {
            return _statesList[stateId];
        }
        return newState;
    }
   
}
