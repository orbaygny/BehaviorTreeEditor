using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutePatrol : ActionNode
{
    public float speed;
    [HideInInspector] public float step;
    float stoppingDistance =0;
    
     public float tolerance ;

     Vector3 startPos;

   static int count = 0;
    private List<Transform> route = new List<Transform>();
    
    protected override  void OnStart()
    {
        if (!agentData.gameObject.GetComponent<AiSensor>().attack)
        {
            Debug.Log(count);
            route = agentData.gameObject.GetComponent<AgentRoute>().routePlacements;
            startPos = agentData.startPos;
            stoppingDistance = tolerance - 0.2f;
            agentData.agent.stoppingDistance = stoppingDistance;

            if (count == route.Count)
            {
                count = 0;
            }
            agentData.agent.SetDestination(new Vector3(
                route[count].position.x,
                agentData.transform.position.y,
                route[count].position.z));

            /*agentData.agent.transform.LookAt(new Vector3(
                route[count].position.x,
                agentData.transform.position.y,
                route[count].position.z));*/
            count++;

            agentData.gameObject.GetComponent<DemoAI>().isRunning = true;
        }
        
    }

    protected override void OnStop()
    {
        //throw new System.NotImplementedException();
    }

    protected override State OnUpdate()
    {
        if (agentData.gameObject.GetComponent<AiSensor>().visibleTargets.Count != 0 || agentData.gameObject.GetComponent<AiSensor>().attack)
        {
            Debug.Log(agentData.gameObject.GetComponent<AiSensor>().visibleTargets.Count);
            //agentData.gameObject.GetComponent<DemoAI>().isRunning = false;

            return State.Failure;

        }
       else if (route.Count == 0 )
        {
            return State.Failure;
        }


      else if (agentData.agent.pathPending ) {
            return State.Running;
        }

        else if (agentData.agent.remainingDistance < tolerance ) {
            
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
