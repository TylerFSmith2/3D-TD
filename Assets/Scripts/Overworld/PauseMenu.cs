using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //If escape is pressed
        {
            pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeInHierarchy); //Toggle the menu to on/off to the opposite of what it is right now
        }
    }

    public void CloseMenu() //Closes panel
    {
        pauseMenu.SetActive(false);
    }

    public void ExitGame() //Close the game
    {
        Application.Quit();
    }

    public void OpenOptionsMenu()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void ReturnToOverworld()
    {
        SceneManager.LoadScene("Overworld", LoadSceneMode.Single);
    }
}
