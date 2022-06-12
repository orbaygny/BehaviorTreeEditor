using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class AgentRoute : MonoBehaviour
{
  public List<Transform> routePlacements = new List<Transform>();
    // Update is called once per frame
     void Update()
    {
        if(Selection.Contains(gameObject))
        {   Debug.DrawLine(transform.position,routePlacements[0].position,Color.blue);
            for(int i = 0; i< routePlacements.Count-1 ; i++)
            {
                Debug.DrawLine(routePlacements[i].position,routePlacements[i+1].position,Color.blue);
            }
        }
       /* if(routePlacements.Count !=0)
        {
            foreach(Transform place in routePlacements)
            {
                if(place == null)
                {
                    routePlacements.Remove(place);
                }
            }
        }*/
    }
}
