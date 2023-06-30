using UnityEngine;
using UnityEngine.AI;

public class FollowWhitwLightCommand : ICommand
{
    NavMeshAgentStates _navMeshAgentStates;
    NavMeshAgent _navMeshAgent;
    Vector3 _lightSource;
    float _navMeshSpeed;
    float _stopDistance;
    public FollowWhitwLightCommand(NavMeshAgentStates navMeshAgentStates, NavMeshAgent navMeshAgent, Vector3 lightSource, float navMeshSpeed, float stopDistance)
    {
        _navMeshAgentStates = navMeshAgentStates;
        _navMeshAgent = navMeshAgent;
        _lightSource = lightSource;
        _navMeshSpeed = navMeshSpeed;
        _stopDistance = stopDistance;
        
    }
    public void Execute()
    {
        _navMeshAgentStates.FollowWhiteLight(_navMeshAgent, _navMeshSpeed, _stopDistance, _lightSource);

    }
}
