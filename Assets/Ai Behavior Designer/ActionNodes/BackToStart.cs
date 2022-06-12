using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToStart : ActionNode
{
    Vector3 startPos;
    Vector3 startRot;
    float stoppingDistance = 0;

    public float tolerance;
    protected override void OnStart()
    {
        //if (agentData.gameObject.GetComponent<AiSensor>().visibleTargets.Count != 0) { OnUpdate(); }
        startPos = agentData.startPos;
        startRot = agentData.startRot;

        stoppingDistance = tolerance - 0.2f;
        agentData.agent.stoppingDistance = stoppingDistance;
        
        

        if (Vector3.Distance(agentData.agent.transform.position,startPos) > stoppingDistance) 
        
        {
            if (!agentData.gameObject.GetComponent<AiSensor>().attack)
            {
                agentData.agent.SetDestination(startPos);
                agentData.gameObject.GetComponent<DemoAI>().isAttacking = false;
                agentData.gameObject.GetComponent<DemoAI>().isRunning = true;
            }
        }
    }

    protected override void OnStop()
    {
       
    }

    protected override State OnUpdate()
    {
        if (agentData.gameObject.GetComponent<AiSensor>().visibleTargets.Count != 0 || agentData.gameObject.GetComponent<AiSensor>().attack)
        {
            Debug.Log(agentData.gameObject.GetComponent<AiSensor>().visibleTargets.Count);
            //agentData.gameObject.GetComponent<DemoAI>().isRunning = false;

            return State.Failure;

        }

        else if(Vector3.Distance(agentData.agent.transform.position, startPos) <= stoppingDistance) 
        {
            //agentData.gameObject.GetComponent<DemoAI>().isRunning = false;
            return State.Failure;
        }
        else if (agentData.agent.pathPending)
        {
            agentData.gameObject.GetComponent<DemoAI>().isRunning = true;
            return State.Running;
        }

        else if (agentData.agent.remainingDistance < tolerance)
        {
            Debug.Log("Buna Girdi");
           // agentData.gameObject.GetComponent<DemoAI>().isRunning = false;
            agentData.transform.rotation = Quaternion.Euler(startRot);
            return State.Success;
        }

        else if (agentData.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            agentData.gameObject.GetComponent<DemoAI>().isRunning = false;
            return State.Failure;
        }
        return State.Running;
    }

    
}
