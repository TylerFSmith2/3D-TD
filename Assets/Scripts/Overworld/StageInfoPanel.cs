using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageInfoPanel : MonoBehaviour
{
    //Stage info
    public VarString StageName;
    public VarString Description;
    public VarInt Difficulty;
    public VarInt level;

    //Strings of the weapon names
    public VarString Weapon1;
    public VarString Weapon2;

    //Helps chose which weapon to update
    bool LastWeaponUpdated = true; 
    
    //Text to update weapon info
    public Text Weapon1Text;
    public Text Weapon2Text;

    // Start is called before the first frame update
    void Start()
    {
        Weapon1.value = "";
        Weapon2.value = "";
    }

    public void CloseMenu() //Closes panel
    {
        this.gameObject.SetActive(false);
    }

    public void StartLevel()
    {
        if(Weapon1.value.Equals(null) || Weapon2.value.Equals(null))
        {
            Debug.Log("No weapons selected");
        }
        else
        {
            SceneManager.LoadScene(level.value.ToString(), LoadSceneMode.Single); //Loads scene of the next stage. REQUIRES STAGE NAME TO BE A NUMBER, GIVEN BY THE CURRENTLEVELSO ITSELF
        }
    }
    public void UpdateInfo()
    {
        Transform[] children = this.gameObject.GetComponentsInChildren<Transform>(); //Get children for looping to change their values
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
    }

    public void ChooseWeapon(string weapon) //Updates the SO's and text fields of the weapons
    {
        //Checks which weapon should be updated with the new one. Choses an open one or whichever one was changed earlier.
        if (Weapon1.value == weapon || Weapon2.value == weapon) //If a weapon is chosen twice
        {
            Debug.Log("Same Weapon");
        }
        else if (Weapon1.value == "") //if first weapon is not yet chosen
        {
            Weapon1.value = weapon;
            Weapon1Text.text = Weapon1.value;
            LastWeaponUpdated = true;
        }
        else if (Weapon2.value == "") //if second weapon is not yet chosen
        {
            Weapon2.value = weapon;
            Weapon2Text.text = Weapon2.value;
            LastWeaponUpdated = false;
        }
        else
        {
            if (LastWeaponUpdated) //if first weapon was last changed, change second weapon next
            {
                Weapon2.value = "";
                Weapon2Text.text = Weapon2.value;
            }
            else //if second weapon was last changed, first second weapon next
            {
                Weapon1.value = "";
                Weapon1Text.text = Weapon1.value;
            }
            ChooseWeapon(weapon);
        }
    }

    public void ClearWeapons() //Clear the weapons and their text fields
    {
        Weapon1.value = null;
        Weapon2.value = null;
        Weapon1Text.text = Weapon1.value;
        Weapon2Text.text = Weapon2.value;
    }
}
