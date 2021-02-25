using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : IHeapItem<PathNode>
{
    public bool isWalkable;
    public Vector3 worldPos;
    public int gridX, gridY;
    
    public int gCost;
    public int hCost;
    public PathNode parent;
    int heapIndex;

    public PathNode(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        isWalkable = _walkable;
        worldPos = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost {get{return gCost + hCost;}}

    public int HeapIndex {
        get {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(PathNode other)
    {
        int compare = fCost.CompareTo(other.fCost);
        if(compare == 0)
        {
            compare = hCost.CompareTo(other.hCost);
        }

        return -compare;
    }
}
