using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemoAI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;

    public bool isRunning,isAttacking,isDead;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {   
             anim.SetTrigger("Dead");
           
            gameObject.GetComponent<BehaviorTreeRunner>().enabled = false;
             gameObject.GetComponent<AiSensor>().enabled = false;
              gameObject.GetComponent<NavMeshAgent>().enabled = false;
              gameObject.GetComponent<CapsuleCollider>().enabled=false;
            
         isDead = false;
          
        
        }
        else
        {
        anim.SetBool("Run",isRunning);
            if (isAttacking) 
            {
                anim.SetTrigger("Hit");
            }

            else { anim.ResetTrigger("Hit"); }
        
        }
        
        
    }
}
