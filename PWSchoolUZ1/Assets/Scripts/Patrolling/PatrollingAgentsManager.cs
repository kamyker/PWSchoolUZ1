using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingAgentsManager : MonoBehaviour
{
    List<PatrollingAgent> agents =
        new List<PatrollingAgent>();

    public void Add( PatrollingAgent agent )
    {
        agents.Add( agent );
    }
    
    public void Remove( PatrollingAgent agent )
    {
        agents.Remove( agent );
    }

    void Update()
    {
        foreach ( var agent in agents)
        {
            agent.SetNextPointIfNeeded();
        }
    }
}