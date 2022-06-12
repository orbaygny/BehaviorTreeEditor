using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AgentDeneme : MonoBehaviour
{
   NavMeshAgent agent;
   Animator anim;
    void Start()
    {
       agent = GetComponent<NavMeshAgent>();
       anim =GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      if  (agent.remainingDistance>agent.stoppingDistance ) {
            anim.SetBool("Walk",true);
            Debug.Log("true");
        }
         
      else 
      {
          anim.SetBool("Walk",false);
            Debug.Log("true");
      }
    }
}
