using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SF : MonoBehaviour
{
    protected StateMachineCtx _context;
    public SF(StateMachineCtx currentContext)
    {
        
    }
    //En este método se deben agregar las instancias de los distintos states del personaje
    public abstract void SetUpStatesList();

    protected string _defaultStateId;
    public string DefaultStateId{set => _defaultStateId = value;}
    protected Dictionary<string, BS> _statesList = new Dictionary<string, BS>();

    public BS GetState(string stateId)
    {   
        BS newState = _statesList[_defaultStateId];
        if( _statesList.ContainsKey(stateId))
        {
            return _statesList[stateId];
        }
        return newState;
    }
   
}
