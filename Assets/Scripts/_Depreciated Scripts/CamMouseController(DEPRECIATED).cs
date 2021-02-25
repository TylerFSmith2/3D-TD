using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Basic Script for primative first person view, now clamps or anything to prevent unwanted movement, 
//Was only intended for testing but could be used if first person view is desired and more polish is added obv 
public class CamMouseController : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 smoothVec; //Smooths movement of mouse
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    GameObject character;
    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        character = this.transform.parent.gameObject; //Get Character from parent object
        cc = character.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //If we arent in the overhead view, we preform the necessary smoothing calculations using the mouse input to rotate the camera as
        //Desired, no clamps exist to prevent excessive rotation
        if (!cc.isOverhead)
        {
            var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

            smoothVec.x = Mathf.Lerp(smoothVec.x, md.x, 1f / smoothing);
            smoothVec.y = Mathf.Lerp(smoothVec.y, md.y, 1f / smoothing);

            mouseLook += smoothVec;

            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right); //Looks up and down
            character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        }
    }
}