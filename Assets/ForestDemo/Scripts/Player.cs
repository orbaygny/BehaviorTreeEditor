using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6f;
    Vector2 turn;

    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 10.0f;
    public float deceleration = 2.0f;
    
    public LayerMask enemyLayer;
    public Transform weapeon;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {  
        if(Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("Attack",true);
            Attack();
            
        }
        if(!animator.GetBool("Attack"))
        {
            // Anismasyon için alınan inputlar
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool backPressed = Input.GetKey("s");

        if(forwardPressed )
        {
            velocityZ = 2;
        }

        if(backPressed)
        {
            velocityZ = -10;
        }
        if(leftPressed )
        {
            velocityX = -10;
        }

        if(rightPressed )
        {
            velocityX =10;
        }

        if(forwardPressed && rightPressed )
        {
            velocityZ =5;
            
        }

        if(forwardPressed && leftPressed)
        {
            velocityZ =5;
        }
        if(!forwardPressed && velocityZ >0.0f)
        {
            velocityZ = 0.0f;
        }

        if(!backPressed && velocityZ <0.0f)
        {
            velocityZ =0.0f;
        }

        if(!leftPressed && velocityX < 0.0f)
        {
            velocityX = 0;
        }
        if(!rightPressed && velocityX>0.0f)
        {
            velocityX =0;
        }
        animator.SetFloat("Velocit Z",velocityZ);
        animator.SetFloat("Velocity X",velocityX);

        //Hareket ve fare ile döndürme
        turn.x += Input.GetAxis("Mouse X");
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal,0,vertical).normalized;
        transform.localRotation =Quaternion.Euler(0,turn.x,0);
        if(direction.magnitude >=0.1f)
        {
            direction = transform.TransformDirection(direction);
            controller.Move(direction*speed*Time.deltaTime);
        }
        }
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(weapeon.position,3f,enemyLayer);
        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponent<DemoAI>().isDead = true;
        }
    }
}
