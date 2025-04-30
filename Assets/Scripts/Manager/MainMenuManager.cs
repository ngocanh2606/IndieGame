using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //"Play Game" button is clicked
    public void PlayGame()
    {
        SceneManager.LoadScene("GameLevel");
    }

    //"Tutorial" button is clicked
    public void StartTutorial()
    {
        SceneManager.LoadScene("TutorialLevel");
    }

    //"Exit" button is clicked
    public void ExitGame()
    {
        Application.Quit();
    }
}
