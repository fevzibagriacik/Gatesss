using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    static bool isGameStartedOnce = false;

    public void PlayButton() 
    {
        if (!isGameStartedOnce)
        {
            isGameStartedOnce = true;
            SceneManager.LoadScene(1);
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SettingsButton()
    {
        // TODO
        // Belki ayarlar ekleriz belki eklemeyiz
    }

    public void ExitButton() 
    {
        Application.Quit();
    }
}
