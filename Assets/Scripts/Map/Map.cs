using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Camera mapCam; //Camera above the map for the map
    public GameObject miniMapQuad; //Camera above the map for the minimap
    public GameObject towerPanel; //Panel that holds the tower buttons
    public VarTower currentTower;
    // Start is called before the first frame update
    void Start()
    {
        mapCam.gameObject.SetActive(!mapCam.gameObject.activeInHierarchy); //Toggle on/off the map
        currentTower.tower = null; //Start game with no tower selected
    }

    
    void Update()
    {
        //If M is pressed, toggle ui elements
        if (Input.GetKeyDown(KeyCode.M))
        {
            toggleMapUI();
        }
    }

    void toggleMapUI()
    {
        miniMapQuad.gameObject.SetActive(!miniMapQuad.gameObject.activeInHierarchy); //Toggle on/off the map
        mapCam.gameObject.SetActive(!mapCam.gameObject.activeInHierarchy); //Toggle on/off the map
        towerPanel.gameObject.SetActive(!towerPanel.gameObject.activeInHierarchy); //Toggle on/off the towerPanel
        if (!towerPanel.activeSelf) //Remove the currently held tower when ui panel is closed
        {
            currentTower.tower = null;
        }
    }
}
