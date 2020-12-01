using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingAgentsManager : MonoBehaviour
{
    HashSet<PatrollingAgent> agents =
        new HashSet<PatrollingAgent>();

    public void Add( PatrollingAgent agent )
    {
        agents.Add( agent );
    }

    public void Remove( PatrollingAgent agent )
    {
        if ( agents.Contains( agent ) )
            agents.Remove( agent );
    }

    void Update()
    {
        foreach ( var agent in agents )
            agent.SetNextPointIfNeeded();
    }
}