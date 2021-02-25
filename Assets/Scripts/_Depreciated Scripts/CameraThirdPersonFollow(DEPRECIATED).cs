using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraThirdPersonFollow : MonoBehaviour
{
    //Attributes of the third person camera
    public Transform playerTrans;
    public Vector3 lookOffset = new Vector3(0,1,0);
    public float distance = 5;
    public float cameraSpeed = 8;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookPosition = playerTrans.position + lookOffset;
        this.transform.LookAt(lookPosition);

        if(Vector3.Distance(this.transform.position, lookPosition) > distance)
        {
            this.transform.Translate(0, 0, cameraSpeed * Time.deltaTime);
        }

    }
}
