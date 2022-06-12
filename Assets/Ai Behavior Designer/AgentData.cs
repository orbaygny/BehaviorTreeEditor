using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentData
{
  
    public GameObject gameObject;
        public Transform transform;
        public Animator animator;
        public Rigidbody physics;
        public NavMeshAgent agent;
        public SphereCollider sphereCollider;
        public BoxCollider boxCollider;
        public CapsuleCollider capsuleCollider;
        public CharacterController characterController;
        
        public Vector3 startPos;
         public Vector3 startRot;



    // Add other game specific systems here

    public static AgentData CreateFromGameObject(GameObject gameObject) {
            // Fetch all commonly used components
            AgentData agentData = new AgentData();
            agentData.gameObject = gameObject;
            agentData.transform = gameObject.transform;
            agentData.animator = gameObject.GetComponent<Animator>();
            agentData.physics = gameObject.GetComponent<Rigidbody>();
            agentData.agent = gameObject.GetComponent<NavMeshAgent>();
            agentData.sphereCollider = gameObject.GetComponent<SphereCollider>();
            agentData.boxCollider = gameObject.GetComponent<BoxCollider>();
            agentData.capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
            agentData.characterController = gameObject.GetComponent<CharacterController>();
            agentData.startPos =gameObject.transform.position;
            agentData.startRot = gameObject.transform.rotation.eulerAngles;
           
            // Add whatever else you need here...

            return agentData;
        }
    }

