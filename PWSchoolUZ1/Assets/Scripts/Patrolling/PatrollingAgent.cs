using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent( typeof(NavMeshAgent) )]
public class PatrollingAgent : MonoBehaviour
{
    PatrolPoint[ ] patrolPoints;
    NavMeshAgent myNavMeshAgent;
    int curPatrolPointIdx = -1;
    
    enum PatrollingMode
    {
        Loop,
        PingPong
    }
    [SerializeField] PatrollingMode patrollingMode = PatrollingMode.Loop;
    [SerializeField] bool goingForwardPingPongMode;
    
    void Awake()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void SetPatrolPoints( PatrolPoint[ ] patrolPoints )
    {
        if ( patrolPoints == null || patrolPoints.Length == 0 )
        {
            Debug.LogWarning( "Patrol points are null or empty. Could be an error." );
            return;
        }

        var closestPoint = FindClosestPoint();

        this.patrolPoints = patrolPoints;
        myNavMeshAgent.destination = closestPoint.pos;
        curPatrolPointIdx = closestPoint.id;

        (Vector3 pos, int id) FindClosestPoint()
        {
            Vector3 agentPos = transform.position;
            Vector3 closestPoint = patrolPoints[0].transform.position;
            int closestPointIdx = 0;
            float closestPointDistanceSqr = (-agentPos + closestPoint).sqrMagnitude;
            for ( int i = 1; i < patrolPoints.Length; i++ )
            {
                Vector3 point = patrolPoints[i].transform.position;
                float distanceToPointSqr = (-agentPos + point).sqrMagnitude;
                if ( distanceToPointSqr < closestPointDistanceSqr )
                {
                    closestPointDistanceSqr = distanceToPointSqr;
                    closestPoint = point;
                    closestPointIdx = i;
                }
            }

            return (closestPoint, closestPointIdx);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine( transform.position, myNavMeshAgent.destination );
    }

    public void SetNextPointIfNeeded()
    {
        if ( curPatrolPointIdx != -1 && !myNavMeshAgent.pathPending && myNavMeshAgent.remainingDistance < 0.5f )
            SetNextPoint();
    }

    void SetNextPoint()
    {
        if ( patrolPoints == null || patrolPoints.Length == 0 )
            return;

        if(patrollingMode == PatrollingMode.Loop)
            curPatrolPointIdx = (curPatrolPointIdx + 1) % patrolPoints.Length;
        else if ( patrollingMode == PatrollingMode.PingPong )
        {
            if ( goingForwardPingPongMode )
            {
                curPatrolPointIdx++;
                if ( curPatrolPointIdx >= patrolPoints.Length )
                {
                    curPatrolPointIdx--;
                    goingForwardPingPongMode = false;
                }
            }
            else
            {
                curPatrolPointIdx--;
                if ( curPatrolPointIdx < 0 )
                {
                    curPatrolPointIdx++;
                    goingForwardPingPongMode = true;
                }
            }
        }
        else
            throw new NotImplementedException();

        myNavMeshAgent.destination = patrolPoints[curPatrolPointIdx].transform.position;
    }
}