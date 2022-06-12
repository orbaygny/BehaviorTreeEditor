using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker : ActionNode
{
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
       if(agentData.gameObject.GetComponent<AiSensor>().visibleTargets.Count != 0 || agentData.gameObject.GetComponent<AiSensor>().attack) 
        {
            return State.Failure;
        }

        return State.Success;
    }
}
