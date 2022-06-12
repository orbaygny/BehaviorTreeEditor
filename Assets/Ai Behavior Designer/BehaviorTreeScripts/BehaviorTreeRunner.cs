using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeRunner : MonoBehaviour
{
   public  BehaviorTree tree;
    AgentData agentData;
    void Start()
    {
       agentData = CreateBehaviourTreeAgentData();
       tree = tree.Clone();
       tree.Bind(agentData);
    }
    void Update()
    {
        tree.Update();
    }
      AgentData CreateBehaviourTreeAgentData() {
            return AgentData.CreateFromGameObject(gameObject);
        }
}
