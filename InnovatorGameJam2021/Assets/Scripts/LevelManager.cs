using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    /// <summary>
    /// Pauses the game by setting timescale to 0
    /// </summary>
    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// Resumes the game by setting timescale to 1
    /// </summary>
    private void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
