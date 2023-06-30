using UnityEngine;
using UnityEngine.AI;
public class ChaseGameObjectCommand : ICommand
{
    NavMeshAgentStates _navMeshAgentStates;
    NavMeshAgent _navMeshAgent;
    Vector3 _gameObjectPosition;
    float _navMeshSpeed;
    float _stopDistance;
    public ChaseGameObjectCommand(NavMeshAgentStates navMeshAgentStates, NavMeshAgent navMeshAgent, Vector3 gameObjectPosition, float navMeshSpeed, float stopDistance)
    {
        _navMeshAgentStates = navMeshAgentStates;
        _gameObjectPosition = gameObjectPosition;
        _navMeshSpeed = navMeshSpeed;
        _stopDistance = stopDistance;
        _navMeshAgent = navMeshAgent;
    }
    public void Execute()
    {
        _navMeshAgentStates.Chase(_navMeshAgent, _gameObjectPosition, _navMeshSpeed, _stopDistance);
    }
    
}