using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 2;
    public float runSpeed = 6;
    public Rigidbody playerRigid;
    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;
    float gravity = 24;
    public bool isGrounded;
    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    float velY;

    Transform cameraT;
    CharacterController controller;
    public float jumpHeight = 1;

    void Start()
    {
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();
    }
    private void OnCollisionStay()
    {
        isGrounded = true;
    }
    // Update is called once per frame
    void Update()
    {Vector3 test = new Vector3(1, 1, 1);
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
        bool running = Input.GetKey(KeyCode.LeftShift);
        
        Move(inputDir, running);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }




    }
    private void Move(Vector2 inputDir, bool running)
    {
        //If our directional vector is not 0,
        if (inputDir != Vector2.zero)
        {
            //Find rotation with the arctangent of our x and y values, convert to degrees and add the eulerAngles of our camera for smoothing.
            //Dampen the smoothing and apply the transformation
            float targetRot = (Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg) + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRot, ref turnSmoothVelocity, turnSmoothTime);
        }
        //Decide which speed value to multiply for the move vector
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        //Determine our current speed by smoothing the value between current and targeted speed with our speedSmoothTime value
        //So our speed accelerates naturally.
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        velY += Time.deltaTime * gravity;
        Vector2 velocity = transform.forward * currentSpeed + Vector3.up * velY;
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
        //controller.Move
        //currentSpeed = new Vector2(controller.velocity.x)
    }
    private void Jump()
    {
        if (isGrounded)
        {
            Debug.Log("Jump");
            //float jumpVelocity = Mathf.Sqrt(gravity * jumpHeight * -2);
            Vector3 jumpTest = new Vector3(0, 2, 0);
            playerRigid.AddForce(jumpHeight*jumpTest, ForceMode.Impulse);
            isGrounded = false;
        }
    }
        
}

