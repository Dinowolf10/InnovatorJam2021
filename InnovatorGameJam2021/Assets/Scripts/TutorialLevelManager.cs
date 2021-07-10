using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelManager : MonoBehaviour
{
    public GameObject jumpText;

    public GameObject wallJumpTextPart1;

    public GameObject wallJumpTextPart2;

    public GameObject environmentText;

    public GameObject waterText;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.timeScale == 0 && Input.GetButtonDown("Jump"))
        {
            jumpText.SetActive(false);

            wallJumpTextPart1.SetActive(false);

            wallJumpTextPart2.SetActive(false);

            environmentText.SetActive(false);

            waterText.SetActive(false);

            ResumeGame();
        }
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="triggerName"></param>
    public void PickTutorialToDisplay(string triggerName)
    {
        if (triggerName == "PauseGameTrigger1")
        {
            DisplayJumpTutorial();

            Debug.Log("Hit first pause trigger");
        }
        else if (triggerName == "PauseGameTrigger2")
        {
            DisplayWallJumpTutorialPart1();

            Debug.Log("Hit second pause trigger");
        }
        else if (triggerName == "PauseGameTrigger3")
        {
            DisplayWallJumpTutorialPart2();

            Debug.Log("Hit third pause trigger");
        }
        else if (triggerName == "PauseGameTrigger4")
        {
            DisplayEnvironmentTutorial();

            Debug.Log("Hit fourth pause trigger");
        }
        else if (triggerName == "PauseGameTrigger5")
        {
            DisplayWaterTutorial();

            Debug.Log("Hit fifth pause trigger");
        }
    }

    /// <summary>
    /// Pauses the game and displays the jump tutorial to the player
    /// </summary>
    private void DisplayJumpTutorial()
    {
        jumpText.SetActive(true);

        PauseGame();
    }

    /// <summary>
    /// 
    /// </summary>
    private void DisplayWallJumpTutorialPart1()
    {
        wallJumpTextPart1.SetActive(true);

        PauseGame();
    }

    /// <summary>
    /// 
    /// </summary>
    private void DisplayWallJumpTutorialPart2()
    {
        wallJumpTextPart2.SetActive(true);

        PauseGame();
    }

    /// <summary>
    /// 
    /// </summary>
    private void DisplayEnvironmentTutorial()
    {
        environmentText.SetActive(true);

        PauseGame();
    }

    private void DisplayWaterTutorial()
    {
        waterText.SetActive(true);

        PauseGame();
    }
}
