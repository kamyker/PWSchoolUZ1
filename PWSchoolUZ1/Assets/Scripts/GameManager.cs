using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] PatrolPoints patrolPoints;
    [SerializeField] PatrollingAgentsManager patrollingAgentsManager;
    [SerializeField] PatrollingAgent patrollingAgentPrefab;
    [SerializeField] Transform spawnPoint;
    
    void Start()
    {
        patrolPoints.GatherPointsFromChildren();
        
        var enemyPatrolling = Instantiate( patrollingAgentPrefab, spawnPoint.position, patrollingAgentPrefab.transform.rotation )
           .GetComponent<PatrollingAgent>();
       enemyPatrolling.SetPatrolPoints( patrolPoints.Points );
       patrollingAgentsManager.Add( enemyPatrolling );
    }
}