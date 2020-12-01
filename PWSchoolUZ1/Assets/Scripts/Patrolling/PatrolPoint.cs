using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    void OnDrawGizmos() //change to OnDrawGizmosSelected if needed
    {
        if ( transform.parent != null )
        {
            int nextChildIdx = (transform.GetSiblingIndex() + 1) % transform.parent.childCount;
            Gizmos.DrawLine( transform.position, transform.parent.GetChild( nextChildIdx ).position );
        }
    }
}
