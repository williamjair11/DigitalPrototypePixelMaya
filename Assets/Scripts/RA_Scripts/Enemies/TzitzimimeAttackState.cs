using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TzitzimimeAttackState : BaseState
{
    private Tzitzimime _contextState;
    
    public TzitzimimeAttackState(StateMachineContext currentContext, StatesFactory stateFactory) : base(currentContext, stateFactory)
    {
        _contextState = (Tzitzimime)currentContext;
    }

    public override void OnStartState()
    {
        _contextState.Agent.isStopped = true;
        _contextState.Animator.SetBool(TzitzimimeAnimationsId.Attacking.ToString(),true);
    }

    public override void OnExitState()
    {
        _contextState.DisableCanMakeDamage();
        _contextState.Animator.SetBool(TzitzimimeAnimationsId.Attacking.ToString(),false);

    }

    public override void Update()
    {
        
        if(_contextState.CurrentEnemyState == TzitzimimeStatesId.Idle)
            SwitchState(_factory.GetState(TzitzimimeStatesId.Idle.ToString()));

        if(_contextState.CurrentEnemyState == TzitzimimeStatesId.Walking)
            SwitchState(_factory.GetState(TzitzimimeStatesId.Walking.ToString()));

        if( _contextState.CurrentEnemyState == TzitzimimeStatesId.Following) 
            SwitchState(_factory.GetState(TzitzimimeStatesId.Following.ToString()));
        if( _contextState.CurrentEnemyState == TzitzimimeStatesId.Attacking)
            RotateTowardsTarget();
           // _contextState.transform.LookAt(GameManager.Instance.playerController.transform.position);

    }

    public void RotateTowardsTarget()
    {
        Vector3 targetDirection = _contextState.Target.transform.position - _contextState.transform.position;
        targetDirection.y = 0; // Esto asegura que solo gire en el eje Y

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // Suaviza la rotación
        _contextState.transform.rotation = Quaternion.Slerp(_contextState.transform.rotation, targetRotation, Time.deltaTime);
    }
}
