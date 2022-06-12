using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;

    public Animator anim;
    Vector3 mousePos;
    Vector3 lookPos;
    public Camera cam;

    public GameObject projectile;
    public Transform projectileSpawn;

    // Start is called before the first frame update
    void Start()
    {
         // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
       
      LookMouse();
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.right* horizontal + transform.forward*vertical;
        controller.Move(move*speed*Time.deltaTime);
        if(Input.GetButtonDown("Fire1")){

             Instantiate(projectile,projectileSpawn.position,transform.localRotation);
        }
        if(Input.GetAxisRaw("Horizontal") !=0 || Input.GetAxisRaw("Vertical")!=0)
        {
            anim.SetFloat("speed",1);
        }
        else{
            anim.SetFloat("speed",0);
        }
    }

    private void LookMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit hitInfo,maxDistance:300f)){
            var target = hitInfo.point;
            transform.LookAt(target);
        }
    }
}
