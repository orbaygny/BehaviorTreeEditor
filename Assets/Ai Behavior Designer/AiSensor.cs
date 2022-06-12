using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AiSensor : MonoBehaviour
{
    public LayerMask targetMask;
    public LayerMask obstacleMask;
   public float viewRadius;
   [Range(0,360)]
   public float viewAngle;
   public bool attack = false;
   public bool moveToTarget = false;

    public List<Transform> visibleTargets = new List<Transform>();

    public bool hitted;

    public float attackDistance;

   

    void Start(){
        StartCoroutine("FindTargetsWithDelay",.2f);
        attackDistance = 2;
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    void FindVisibleTargets()
    {
       // visibleTargets.Clear();
        attack = false;
      
        Collider [] targetInViewRadius = Physics.OverlapSphere(transform.position,viewRadius,targetMask);

        for(int i = 0; i < targetInViewRadius.Length; i++)
        {
           
            Transform target =targetInViewRadius[i].transform;
            Vector3 directionToTarget = (target.position- transform.position).normalized;
            if(Vector3.Angle(transform.forward,directionToTarget)<viewAngle/2 ){
               float distanceToTarget = Vector3.Distance(transform.position,target.position); 

               if(!Physics.Raycast(transform.position,directionToTarget,distanceToTarget,obstacleMask) && distanceToTarget <= viewRadius)
               {
                   
                   if(!attack && !visibleTargets.Contains(target)){
                       
                        visibleTargets.Add(target);
                   }
                   attack = true;
  
               }

                else { visibleTargets.Remove(target); }
            }

            else{visibleTargets.Remove(target);}
        }
        
    }
   public Vector3 DirectionFromAngle(float angleInDegrees,bool angleIsGlobal)
   {
       if(!angleIsGlobal)
       {
           angleInDegrees += transform.eulerAngles.y;
       }
       return new Vector3(Mathf.Sin(angleInDegrees*Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees*Mathf.Deg2Rad));

   }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            hitted =true;
        }
    }
   
   
}
