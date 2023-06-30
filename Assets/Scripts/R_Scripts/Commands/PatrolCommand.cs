using UnityEngine;
using UnityEngine.AI;

public class PatrolCommand : ICommand
{
    NavMeshAgentStates _navMeshAgentStates;
    NavMeshAgent _navMeshAgent; 
    Transform _currentWaypoint;
    float _navMeshSpeed;
    float _stopDistance;
    public PatrolCommand(NavMeshAgentStates navMeshAgentStates, NavMeshAgent navMeshAgent, Transform currentWaypoint, float navMeshSpeed, float stopdDistance)
    {
        _navMeshAgentStates = navMeshAgentStates;
        _navMeshAgent = navMeshAgent;
        _currentWaypoint = currentWaypoint;
        _navMeshSpeed = navMeshSpeed;
        _stopDistance = stopdDistance;
    }
    public void Execute()
    {
        _navMeshAgentStates.Patrol(_navMeshAgent, _currentWaypoint, _navMeshSpeed, _stopDistance);
    }
}
