using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    public VarTower CurrentTower; //Holds current tower
    public VarInt CurrentMoney; //Players money
    public bool hasTower = false; //If tower exists on this spot
                                  // Start is called before the first frame update



    //TEST -- REMOVE ONCE TOWER CAN BE CLICKED ON 
    public TowerFunctionalityMenu towerFuncMenu;
    //TEST -- REMOVE ONCE TOWER CAN BE CLICKED ON 

    void Start()
    {
        //TEST -- REMOVE ONCE TOWER CAN BE CLICKED ON 
        towerFuncMenu = FindObjectOfType<TowerFunctionalityMenu>();
        //TEST -- REMOVE ONCE TOWER CAN BE CLICKED ON 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        //If the current tower (from the SO) is not null && if the players money is more than or equal to the towers cost && if the spot is open
        Debug.Log("TowerSpotClicked");
        if (CurrentTower.tower != null && CurrentMoney.value >= CurrentTower.tower.stats.cost.value && !hasTower)
        {
            BuildTower();
        }
        
    }

    public void BuildTower() //Builds a tower with the Tower the SO CurrentTower is holding
    {
        Tower newTower = Instantiate(CurrentTower.tower, this.transform.position, this.transform.rotation); //Create new tower at tower spot

        //TEST -- REMOVE ONCE TOWER CAN BE CLICKED ON 
        towerFuncMenu.OpentowerFunctionalityMenu(newTower);
        //TEST -- REMOVE ONCE TOWER CAN BE CLICKED ON 


        newTower.myTowerSpot = this;
        hasTower = true; //tower exists at this spot
        CurrentMoney.value -= CurrentTower.tower.stats.cost.value;
    }

    public void BuildTower(Tower towerToBuild) //Builds a tower with the given Tower towerToBuild
    {
        Tower newTower = Instantiate(towerToBuild, this.transform.position, this.transform.rotation); //Create new tower at tower spot
        newTower.myTowerSpot = this;
        hasTower = true; //tower exists at this spot
        CurrentMoney.value -= CurrentTower.tower.stats.cost.value;
    }

    //If a tower is removed, it is free for other towers TODO: Towers should have a reference to their TowerSpots to call this
    public void TowerRemoved() 
    {
        hasTower = false;
        //Does it cost money to remove?
    }

    public void UpgradeTower(Tower oldTower, Tower towerToBuild)
    {
        if (CurrentMoney.value >= towerToBuild.stats.cost.value && !hasTower)
        {
            Destroy(oldTower.gameObject); //Destroy the old tower
            BuildTower(towerToBuild); //Make the new tower if possible
        }
    }
}
