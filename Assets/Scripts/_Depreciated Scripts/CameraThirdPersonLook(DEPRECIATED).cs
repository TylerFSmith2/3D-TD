using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Originally was going to be 3rd person camera, decided to try more dynamic approach but this may be useful for "looking" around in 3rd person
//As opposed to looking around in first person, still playing with different types of camera math, might just combine this script and the dynamic one?
public class CameraThirdPersonLook : MonoBehaviour
{
    //Speed of camera Movement
    public float CameraSpeed = 120.0f;
    //Object Camera will focus on
    public GameObject CameraFocus;
    //Vector of the position of the camera
    Vector3 FollowPos;
    //Default clamp angle, atm is set to 80 for all directions. -/+
    public float clampAngle = 80.0f;

    //Sensitivity for camera movement from input
    public float inputSensitivity = 150.0f;
    //Game Object to hold ref the camera
    public GameObject CameraObj;
    //Game Object to hold ref to player
    public GameObject PlayerObj;
    //Floats to calculate distance from camera to player
    public float camXfromPlayer;
    public float camYfromPlayer;
    public float camZfromPlayer;


    //Used for getting the input
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;
    //Used to smooth out movement of the camera
    public float smoothX;
    public float smoothY;

    //Holds rotation of the camera with respect to y and X
    public float rotY = 0.0f;
    public float rotX = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        //Grabs initial rotation
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;

        //Sets state of cursor to be locked to center and invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame, updates the position every frame 
    void Update()
    {
        //Check back later for sticks
        float inputX = Input.GetAxis("RightStickHorizontal");
        float inputZ = Input.GetAxis("RightStickVertical");

        //Retrieves inpurs for the mouse
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        //Combines input from mouse + joystick which is not set up yet
        finalInputX = inputX + mouseX;
        finalInputZ = inputZ + mouseY;

        //Updates rotation using sensitivity relative to the change in the scenes time
        rotY += finalInputX * inputSensitivity * Time.deltaTime;
        rotX += finalInputZ * inputSensitivity * Time.deltaTime;

        //Clamps the rotation of X and Y
        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        rotY = Mathf.Clamp(rotY, -clampAngle, clampAngle);

        //Creates new rotation from previous calculations and then applies it
        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);

        transform.rotation = localRotation;

    }

    //Occurs frame after update, updates the camera using the new position
    private void LateUpdate()
    {
        CameraUpdate();
    }

    //Handles updating the transform of the camera
    void CameraUpdate()
    {
        //Set target to follow
        Transform target = CameraFocus.transform;
        //move towards object
        float step = CameraSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
