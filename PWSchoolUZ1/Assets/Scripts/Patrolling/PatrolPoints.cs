using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoints : MonoBehaviour
{
    public PatrolPoint[] Points { get; private set; }
    
    public void GatherPointsFromChildren()
    {
        Points = GetComponentsInChildren<PatrolPoint>();
    }
}
