using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void PlayButton() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
