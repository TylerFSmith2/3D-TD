using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class used to make grid relative to the map, can be resized but all points will always be equidistant.
//Likely will go unused but in the event it is revisited, this may serve a potential location for
//Level data (where tiles can and cannot be placed) more likely than not a new script will be written
public class Grid : MonoBehaviour
{

    [SerializeField]
    //Size of grid cells
    private float size = 1f;

    public float Size { get { return size; } }
    
    //Retrieves relative point in 3d space snapping to the nearest point on the grid
    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        result += transform.position;

        return result;
    }
    //DEBUG method for drawing spheres in scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for(float i = 0; i < 40; i+= size)
        {
            for(float j = 0; j < 40; j += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(i, 0f, j));
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }

}
