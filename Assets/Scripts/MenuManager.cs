using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject deathMenu;

    public static bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        deathMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                DeactivatePauseMenu();
            }
            else
            {
                ActivatePauseMenu();
            }
        }
    }

    public void ActivateDeathMenu()
    { 
        deathMenu.SetActive(true);
    }

    public void ActivatePauseMenu()
    { 
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void DeactivateDeathMenu()
    {
        deathMenu.SetActive(false);
    }

    public void DeactivatePauseMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Debug.Log("Button Preseeee;");
    }


}
