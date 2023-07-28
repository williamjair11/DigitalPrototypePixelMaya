using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NahualStateFactory : StatesFactory
{
    public NahualStateFactory(StateMachineContext context) : base (context)
    {
        _context = context;
        SetUpStatesList();
    }
    public override void SetUpStatesList()
    {
        _statesList.Add(NahualStates.Idle.ToString(), new NahualIdleState(_context, this));
        _statesList.Add(NahualStates.Patrol.ToString(), new NahualPatrolState(_context, this));
        _statesList.Add(NahualStates.Follow.ToString(), new NahualFollowState(_context, this));
        _statesList.Add(NahualStates.Attack.ToString(), new NahualAttackState(_context, this));
    }
}
