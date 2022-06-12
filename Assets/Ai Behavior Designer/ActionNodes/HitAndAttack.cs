using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAndAttack : ActionNode
{
    [HideInInspector]public Vector3 moveToPosition;
    public float tolerance ;

    [HideInInspector] float distance ;
    
    float stoppingDistance = 0f;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        return State.Failure;
    }
}
