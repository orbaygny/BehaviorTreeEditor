using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPatrol : ActionNode
{
    public float speed ;
    [HideInInspector]public float step;

    public float rMin; 
    public float rMax;
    
    float xPos;
    float zPos;
    [HideInInspector]public Vector3 moveToPosition;

    float stoppingDistance =0;
    
     public float tolerance ;

     public bool snapStart;
     Vector3 startPos;
    
    protected override void OnStart()
    {
      if(agentData.gameObject.GetComponent<AiSensor>().visibleTargets.Count != 0){OnUpdate();}
     startPos = agentData.startPos;

      stoppingDistance = tolerance-0.2f;
      agentData.agent.stoppingDistance = stoppingDistance;
      
      xPos = Random.Range(-rMax,rMax);
      zPos = Random.Range(-rMax,rMax);

      if(xPos>0 && xPos<rMin){
         xPos = Random.Range(rMin,rMax);
      }

      if(xPos<0 && xPos>-rMin){

        xPos = Random.Range(-rMax,-rMin);
      }

      if(zPos>0 && zPos<rMin){
         zPos = Random.Range(rMin,rMax);
      }

      if(zPos<0 && zPos>-rMin){

        zPos = Random.Range(-rMax,-rMin);
      }
     
     if(snapStart)
     {  
       if(!agentData.gameObject.GetComponent<AiSensor>().attack)
       {
        moveToPosition = new Vector3(startPos.x+xPos,agentData.transform.position.y,startPos.z+zPos);
        agentData.agent.SetDestination(moveToPosition);
       }
       
      agentData.gameObject.GetComponent<DemoAI>().isAttacking = false;
       agentData.gameObject.GetComponent<DemoAI>().isRunning = true;
     }
     
     else{
       if(!agentData.gameObject.GetComponent<AiSensor>().attack)
       {
          moveToPosition = new Vector3(agentData.gameObject.transform.position.x+xPos,agentData.transform.position.y,agentData.gameObject.transform.position.z+zPos);
          agentData.agent.SetDestination(moveToPosition);
       }
       
        agentData.gameObject.GetComponent<DemoAI>().isAttacking = false;
       agentData.gameObject.GetComponent<DemoAI>().isRunning = true;
     }
      
    }

    protected override void OnStop()
    {
     
    }

    protected override State OnUpdate()
    {
      if(agentData.gameObject.GetComponent<AiSensor>().visibleTargets.Count != 0 || agentData.gameObject.GetComponent<AiSensor>().attack)
      {
        Debug.Log(agentData.gameObject.GetComponent<AiSensor>().visibleTargets.Count);
        //agentData.gameObject.GetComponent<DemoAI>().isRunning = false;
        
        return State.Failure;
        
      }
     else  if (agentData.agent.pathPending) {
            return State.Running;
        }

       else if (agentData.agent.remainingDistance < tolerance) {
            
            agentData.gameObject.GetComponent<DemoAI>().isRunning = false;
            return State.Success;
        }

       else if (agentData.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid) {
          agentData.gameObject.GetComponent<DemoAI>().isRunning = false;
            return State.Failure;
        }
       return State.Running;
    }
}
