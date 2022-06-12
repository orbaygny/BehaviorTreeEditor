using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    float speed = 25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.forward*speed*Time.fixedDeltaTime;
    }

     private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "AI")
        {
            Debug.Log("DEAD");
            other.gameObject.GetComponent<DemoAI>().isDead = true;
            Destroy(gameObject);
        }
    }
}
