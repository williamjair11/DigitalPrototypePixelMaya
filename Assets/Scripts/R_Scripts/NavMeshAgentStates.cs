using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentStates : MonoBehaviour
{
    #region Waypoints
        Waypoints _waypoints;
    #endregion

   EnergyBallScript _energyBallScript;
    public void Chase(NavMeshAgent navMeshAgent, Vector3 gameObjectPosition, float navMeshSpeed, float stopDistance)
    {
        navMeshAgent.speed = navMeshSpeed;
        navMeshAgent.SetDestination(gameObjectPosition);

        Vector3 direction = (gameObjectPosition - transform.position).normalized;
        Vector3 targetPosition = gameObjectPosition - direction * stopDistance;

        if (Vector3.Distance(transform.position, targetPosition) < stopDistance)
        {
            navMeshAgent.speed = 0f;
        }
        Debug.Log("Chasing");
    }
    public void Patrol(NavMeshAgent navMeshAgent, Transform currentWaypoint, float navMeshSpeed, float stopDistance)
    {
        navMeshAgent.speed = navMeshSpeed;
        navMeshAgent.SetDestination(currentWaypoint.position);

        Vector3 direction = (currentWaypoint.position - transform.position).normalized;
        Vector3 targetPosition = currentWaypoint.position - direction * stopDistance;
        if (Vector3.Distance(transform.position, targetPosition) < stopDistance)
        {
            StartCoroutine(Observing(navMeshAgent, 2f, 4f, navMeshSpeed));
            currentWaypoint = _waypoints.GetNextWaypoint(currentWaypoint);
            navMeshAgent.SetDestination(currentWaypoint.position);
        }
        Debug.Log("Patroling");
    }
    IEnumerator Observing(NavMeshAgent navMeshAgent, float minObservingTime, float maxObservingTime, float navMeshSpeed)
    {
        float observingTime = Random.Range(minObservingTime, maxObservingTime);
        navMeshAgent.speed = 0f;
        yield return new WaitForSeconds(observingTime);
        navMeshAgent.speed = navMeshSpeed;

    }
    public void FollowWhiteLight(NavMeshAgent navMeshAgent, float navMeshSpeed, float stopDistance, Vector3 lightSource)
    {
        navMeshAgent.speed = navMeshSpeed;
        navMeshAgent.SetDestination(lightSource);
        _energyBallScript.DesactivateEnergy();


    }
    public void StunEnemy(float timeStun, bool canMove)
    {
        canMove = false;
        StartCoroutine(stun(timeStun, canMove));
    }

    public IEnumerator stun(float time, bool canMove)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
    }
}
