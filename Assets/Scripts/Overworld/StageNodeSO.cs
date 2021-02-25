using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Tower Spot", menuName = "Tower Spot")]
//The actual SO for the tower spots
public class StageNodeSO : ScriptableObject
{
    public new string name;
    public int level;
    public int difficulty;
    public string description;
}
