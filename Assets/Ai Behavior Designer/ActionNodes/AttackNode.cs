using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : ActionNode
{   
    [HideInInspector] public float distance;
    [HideInInspector] public bool attacking = false;
    public float attackDistance;
    protected override void OnStart()
    {
        
     // if(agentData.gameObject.GetComponent<AiSensor>().attack)
     if(agentData.gameObject.GetComponent<AiSensor>().visibleTargets.Count > 0)
      {
         
          distance = Vector3.Distance(agentData.transform.position,agentData.gameObject.GetComponent<AiSensor>().visibleTargets[0].position);
          if(distance<=attackDistance)
          {
              Debug.Log("attacking");
             attacking = true;
              agentData.gameObject.GetComponent<DemoAI>().isAttacking = true;
                agentData.transform.LookAt(agentData.gameObject.GetComponent<AiSensor>().visibleTargets[0]);

          }

          else
          {
              attacking = false;
              agentData.gameObject.GetComponent<DemoAI>().isAttacking = false;
          }
      }

     
    }

    protected override void OnStop()
    {
       
    }

    protected override State OnUpdate()
    {
        if(agentData.gameObject.GetComponent<AiSensor>().visibleTargets.Count == 0 || distance>attackDistance )
        {
            agentData.gameObject.GetComponent<DemoAI>().isAttacking = false;
            return State.Failure;
        }
        if(attacking)
        {//agentData.gameObject.GetComponent<DemoAI>().isAttacking = false;
           return State.Success;
        }
        
        return State.Failure;
    }
}
