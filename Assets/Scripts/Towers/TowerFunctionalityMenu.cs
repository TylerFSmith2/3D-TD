using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFunctionalityMenu : MonoBehaviour
{
    //The menu for the towers
    public GameObject towerFunctionalityMenu;

    //The tower the menu is currently on
    public Tower currentlyOnTower;

    //The two different paths the tower can upgrade to
    public Tower towerUpgrade1;
    public Tower towerUpgrade2;

    //Camera to do math based on
    public Camera cameraMap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpentowerFunctionalityMenu(Tower tower)
    {
        currentlyOnTower = tower;
        Debug.Log(tower);
        Vector3 menuPos = cameraMap.WorldToScreenPoint(currentlyOnTower.transform.position);
        towerFunctionalityMenu.transform.position = menuPos;
        Debug.Log(menuPos);

        //TODO: Get towers upgrade paths


        //TODO: Set tower menu buttons to spawn those towers
        //towerUpgrade1 = tower;
        //towerUpgrade2 = tower;

        towerFunctionalityMenu.gameObject.SetActive(true);
    }

    public void UpgradeTowerPath1()
    {
        Debug.Log("Need to get the first tower upgrade tree");
        //currentlyOnTower.myTowerSpot.UpgradeTower(currentlyOnTower, towerUpgrade1);
    }

    public void UpgradeTowerPath2()
    {
        Debug.Log("Need to get the second tower upgrade tree");
        //currentlyOnTower.myTowerSpot.UpgradeTower(currentlyOnTower, towerUpgrade2);
    }

    public void SellTower()
    {
        currentlyOnTower.SellTower();
        CloseMenu();
    }

    public void CloseMenu()
    {
        towerFunctionalityMenu.SetActive(false);
    }
}
