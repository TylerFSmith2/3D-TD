using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSideBar : MonoBehaviour
{
    public VarTower currentTower;
    public Text towerDisplay;

    //Updates the current tower on button press from the TowerSideBar - must set buttons to call this
    public void UpdateCurrentTower(Tower t)
    {
        currentTower.tower = t;
        towerDisplay.text = "Current Tower: " + currentTower.tower.name;
    }
    //Removes the current tower from the SO and changes the text to reflect that
    public void ClearTower()
    {
        currentTower.tower = null;
        towerDisplay.text = "No Tower Selected.";
    }
}
