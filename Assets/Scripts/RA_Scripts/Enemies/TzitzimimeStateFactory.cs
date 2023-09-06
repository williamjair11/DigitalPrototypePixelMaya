using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TzitzimimeStateFactory : StatesFactory
{
    public TzitzimimeStateFactory(StateMachineContext currentContext) : base(currentContext)
    {
        _context = currentContext;
        SetUpStatesList();
    }

    public override void SetUpStatesList()
    {
        _statesList.Add(TzitzimimeStatesId.Idle.ToString(), new TzitzimimeIdleState(_context, this));
        _statesList.Add(TzitzimimeStatesId.Walking.ToString(), new TzitzimimeWalkState(_context, this));
        _statesList.Add(TzitzimimeStatesId.Following.ToString(), new TzitzimimeFollowState(_context, this));
        _statesList.Add(TzitzimimeStatesId.Attacking.ToString(), new TzitzimimeAttackState(_context, this));
        _statesList.Add(TzitzimimeStatesId.Stuned.ToString(), new TzitzimimeStunedState(_context, this));
    }
}
