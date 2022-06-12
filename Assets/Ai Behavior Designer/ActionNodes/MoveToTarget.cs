using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : ActionNode
{
    [HideInInspector]public Vector3 moveToPosition;
    public float tolerance ;

    [HideInInspector] float distance ;
    
    float stoppingDistance = 0f;
    protected override void OnStart()
    { 
        stoppingDistance = tolerance-0.4f;
        agentData.agent.stoppingDistance = stoppingDistance;
      

      if(agentData.gameObject.GetComponent<AiSensor>().visibleTargets.Count > 0){
            
        distance = Vector3.Distance(agentData.transform.position,agentData.gameObject.GetComponent<AiSensor>().visibleTargets[0].position);
    
         if(distance>tolerance ){
            
             moveToPosition = agentData.gameObject.GetComponent<AiSensor>().visibleTargets[0].position;
             agentData.agent.SetDestination(moveToPosition);
             Debug.Log("girdi");
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
        if(agentData.gameObject.GetComponent<AiSensor>().visibleTargets.Count == 0)
        {
             agentData.gameObject.GetComponent<DemoAI>().isRunning = false;
            return State.Failure;
        }

         if (agentData.agent.pathPending) {
            return State.Running;
        }

        if (agentData.agent.remainingDistance < tolerance ) {
             //agentData.gameObject.GetComponent<DemoAI>().isRunning = false;
            return State.Success;
        }

        

        if (agentData.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid) {
             agentData.gameObject.GetComponent<DemoAI>().isRunning = false;
            return State.Failure;
        }
       return State.Failure;
    }
}
