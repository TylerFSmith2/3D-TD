using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotSpeed = 30.0f;
    public bool isOverhead;

    //First Person Camera View
    public Camera FPCamera;
    //Overhead Camera
    public Camera OHCamera;

   //Hides and Locks mouse cursor
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        FPCamera.enabled = true;
        OHCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Changes state of mouse dependant on the perspective, overhead or player view (initially was first person when 3rd works properly ill rename it)
        if (Input.GetKeyDown(KeyCode.C))
        {
            FPCamera.enabled = !FPCamera.enabled;
            OHCamera.enabled = !OHCamera.enabled;
            isOverhead = !isOverhead;
            if (isOverhead)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (!isOverhead)
        {
            float translation = Input.GetAxis("Vertical") * speed;
            float straffe = Input.GetAxis("Horizontal") * rotSpeed;
            //Syncs Update movement with gameTime
            translation *= Time.deltaTime;
            straffe *= Time.deltaTime;

            transform.Translate(0, 0, translation);
            transform.Rotate(0, straffe, 0);

            
        }
       
    }
}
