using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a proof of concept object placer that uses grid to instantiate a given prefab into the wortld using the resizable grid
//Seems like it will not be used, but if revisited this script should prolly have functionality added to check terrain, or 
//Placeability of towers, either by looking up a dict of level data or some other method (which would likely result in a new script anyways)
public class ObjectPlacer : MonoBehaviour
{
    //Refs to the grip, overhead camera and prefab to place, in 
    //Practice this script wouldnt be only placing 1 type of prefab obv
    private Grid grid;
    public Camera topDown;
    public GameObject tower;
    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }
    

    // Update is called once per frame
    //Casts a rau and places an object at relative point on the grid
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = topDown.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out hitInfo))
            {
                PlaceObjectNear(hitInfo.point);
            }
        }
    }
    //Used to instantiate prefab for grid placement
    private void PlaceObjectNear(Vector3 clickpoint)
    {
        var finalPosition = grid.GetNearestPointOnGrid(clickpoint);
        // GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
        Instantiate(tower, finalPosition, Quaternion.identity);
    }
}
