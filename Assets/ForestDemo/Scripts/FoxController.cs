using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    public static FoxController Instance { get; private set; }
    float x;
    Animator animator;
    Rigidbody rigidbody;
    public bool move;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        move = true;
        animator = GetComponent<Animator>();
       rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            animator.SetBool("Run", false);
            animator.SetTrigger("Hit");
        }
        else if ( move)
        {
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool("RunBack", false);
                animator.SetBool("Run", true);
                //Vector3 forward = transform.TransformDirection(Vector3.forward);
                //transform.Translate(Vector3.forward * 8 * Time.deltaTime, Space.Self);
                //transform.position += transform.forward*3*Time.deltaTime;

                //rigidbody.velocity = transform.forward * 8;
            }


            else if (Input.GetKey(KeyCode.S))
            {
                animator.SetBool("Run", false);
                animator.SetBool("RunBack", true);
                //Vector3 forward = transform.TransformDirection(Vector3.forward);
                //transform.Translate(Vector3.forward * -8 * Time.deltaTime, Space.Self);
                //transform.position += transform.forward*3*Time.deltaTime;

              //  rigidbody.velocity = transform.forward * -8;
            }

            else { animator.SetBool("Run", false); animator.SetBool("RunBack", false); }
        }

        

        x += Input.GetAxis("Mouse X");
        transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, x, transform.rotation.eulerAngles.z);
    }

    private void FixedUpdate()
    {
        if(move)
        {
            if (Input.GetKey(KeyCode.W))
            {
                //animator.SetBool("RunBack", false);
                //animator.SetBool("Run", true);
                //Vector3 forward = transform.TransformDirection(Vector3.forward);
                //transform.Translate(Vector3.forward * 8 * Time.deltaTime, Space.Self);
                //transform.position += transform.forward*3*Time.deltaTime;

                rigidbody.velocity = transform.forward * 400 * Time.fixedDeltaTime;
            }


            else if (Input.GetKey(KeyCode.S))
            {
                //animator.SetBool("Run", false);
                //animator.SetBool("RunBack", true);
                //Vector3 forward = transform.TransformDirection(Vector3.forward);
                //transform.Translate(Vector3.forward * -8 * Time.deltaTime, Space.Self);
                //transform.position += transform.forward*3*Time.deltaTime;

                rigidbody.velocity = transform.forward * -400 *Time.fixedDeltaTime;
            }

            else { rigidbody.velocity = Vector3.zero; }
        }

        else { rigidbody.velocity = Vector3.zero; }
    }
}
