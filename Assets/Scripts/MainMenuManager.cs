using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Call this method when the "Play" button is clicked
    public void PlayGame()
    {
        SceneManager.LoadScene("GameLevel"); // Replace with your actual game scene name
    }

    // Call this method when the "Tutorial" button is clicked
    public void StartTutorial()
    {
        SceneManager.LoadScene("TutorialLevel"); // Replace with your tutorial scene name
    }

    // Call this method when the "Exit" button is clicked
    public void ExitGame()
    {
        Application.Quit();
    }
}
