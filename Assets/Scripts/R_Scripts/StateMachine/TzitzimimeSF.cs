using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TzitzimimeSF : SF
{
    public TzitzimimeSF(StateMachineCtx currentContext) : base(currentContext)
    {
        _context = currentContext;
        SetUpStatesList();
    }

    public override void SetUpStatesList()
    {
        _statesList.Add(TzitzimimeStatesId.Idle.ToString(), new IdleState(_context, this));
        _statesList.Add(TzitzimimeStatesId.Walking.ToString(), new WalkState(_context, this));
        _statesList.Add(TzitzimimeStatesId.Following.ToString(), new FollowState(_context, this));
        _statesList.Add(TzitzimimeStatesId.Attacking.ToString(), new AttackState(_context, this));
    }
}
