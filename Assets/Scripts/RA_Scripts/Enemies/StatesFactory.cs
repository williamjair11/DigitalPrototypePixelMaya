using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatesFactory
{
    protected StateMachineContext _context;
    public StatesFactory(StateMachineContext currentContext)
    {
        
    }
    //En este método se deben agregar las instancias de los distintos states del personaje
    public abstract void SetUpStatesList();

    protected string _defaultStateId;
    public string DefaultStateId{set => _defaultStateId = value;}
    protected Dictionary<string, BaseState> _statesList = new Dictionary<string, BaseState>();

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
