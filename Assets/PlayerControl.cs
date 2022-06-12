using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float mouseSensitivity = 2000f;
    public Transform playerBody;

    CharacterController controller;
    public float speed = 10f;

    public Transform gunPoint;

    public GameObject bullet;

    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

     
        playerBody.Rotate(Vector3.up * mouseX);


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right*x+transform.forward*z;

        controller.Move(move*speed*Time.deltaTime);

        if(Input.GetMouseButton(0))
        {
            Instantiate(bullet,gunPoint.position,gunPoint.rotation);
        }
    }
}
