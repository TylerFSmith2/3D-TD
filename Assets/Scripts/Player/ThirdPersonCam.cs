using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public float mSensitivity = 10;
    public Transform target;
    public float distanceFromTarget = 2;
    public Vector2 camClamp = new Vector2(-40, 85);

    public float rotSmoothTime = .12f;
    Vector3 rotSmoothVel;
    Vector3 currentRot;
    float yaw;
    float pitch;
    public bool lockCursor;
    //Start Makes cursor invisible
    private void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    //Rough Zoom Function needs smoothing
    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            if (distanceFromTarget <= 20 || distanceFromTarget > 0)
            {
                distanceFromTarget += 1;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (distanceFromTarget <= 20 || distanceFromTarget >= 0)
            {
                distanceFromTarget -= 1;
            }
        }
    }
    // Gets Mouse input and moves the camera acordingly, clamping it between
    // camClamp and smoothing it with rotSmoothVel
    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mSensitivity;
        pitch = Mathf.Clamp(pitch, camClamp.x, camClamp.y);

        currentRot = Vector3.SmoothDamp(currentRot, new Vector3(pitch, yaw), ref rotSmoothVel, rotSmoothTime);

        
        transform.eulerAngles = currentRot;

        transform.position = target.position - transform.forward * distanceFromTarget;
    }

}
