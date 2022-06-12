using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class RoutePlacer : MonoBehaviour
{
    [SerializeField]
    private GameObject Agent;

    

    // Start is called before the first frame update
    void Awake()
    {
        if(gameObject.GetComponent<DontDestroy>() == null){gameObject.AddComponent<DontDestroy>();}
    }

    // Update is called once per frame
    void Update()
    {
       if( Agent!= null)
       {
           if(!Agent.GetComponent<AgentRoute>().routePlacements.Contains(transform)){ AddToList();}
          
       }

        
    }

    void OnDestroy()
    {
        Agent.GetComponent<AgentRoute>().routePlacements.Remove(transform);
    }

    void AddToList()
    {
    Agent.GetComponent<AgentRoute>().routePlacements.Add(transform);
    
    }
}
