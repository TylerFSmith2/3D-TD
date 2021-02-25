using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Normal movement of camera TODO: Decide on speed, camera distance from world, and if it should be locked on a node
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.up * 3);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.down * 3);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * 3);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * 3);
        }

        //Scroll out feature
    }
}
