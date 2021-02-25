using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TowerSpotDisplay : MonoBehaviour
{
    public StageNodeSO towerSpot; //StageNode SO - holds the current level
    public GameObject StageInfoPanel; //Panel reference to update the info with the current level info

    public VarString StageName; //Current stage name
    public VarString Description; //Current stage description
    public VarInt Difficulty; //Current stage difficulty
    public VarInt level; //Current stage level

    public GameObject pauseMenu; //Pause menu gameobject

    // Start is called before the first frame update
    void Start()
    {
    }
    public void OnMouseDown()
    {
        if (!pauseMenu.gameObject.activeInHierarchy) //if the pause menu is not open
        {
            //Change the values of the SOs to the values in the Stage SO
            StageName.value = towerSpot.name;
            Description.value = towerSpot.description;
            Difficulty.value = towerSpot.difficulty;
            level.value = towerSpot.level;
            OpenMenu(); //On click the tower spot opens the menu
        }
        
    }
    public void OpenMenu()
    {
        //StageInfoPanel.UpdateInfo();
        //Check if level has changed? Lets us skip all this if it has not
        Transform[] children = StageInfoPanel.gameObject.GetComponentsInChildren<Transform>(); //Get children for looping to change their values
        foreach (Transform c in children) //Go through children and change their values for the current stage
        {
            if (string.Compare(c.tag, "StageName") == 0)
            {
                c.GetComponent<Text>().text = StageName.value;
            }
            else if (string.Compare(c.tag, "StageDescription") == 0)
            {
                c.GetComponent<Text>().text = Description.value;
            }
            else if (string.Compare(c.tag, "StageDifficulty") == 0)
            {
                c.GetComponent<Text>().text = Difficulty.value.ToString();
            }
        }
        StageInfoPanel.SetActive(true); //Activates Panel with correct info
    }
}
