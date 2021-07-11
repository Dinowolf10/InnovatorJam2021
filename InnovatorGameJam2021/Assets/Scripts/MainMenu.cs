using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Loads the first level from the Main Menu
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    /// <summary>
    /// Exits the game from the Main Menu
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
