using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winText;  
    [SerializeField] private TextMeshProUGUI loseText; 
    [SerializeField] private Button mainMenuButton; 
    [SerializeField] private GameObject[] gameObjects; 
    [SerializeField] private GameObject playerCharacter; 

    [SerializeField] private Button[] actionButtons;

    [SerializeField] private PlayerAnimation playerAnimation;

    private bool isWin;



    // Method to call when the player loses
    public void Lose()
    {
        playerAnimation.SetState(PlayerCharacterState.Die);
        StartCoroutine(WaitForDeathAnimation());

    }

    // Method to call when the player wins
    public void Win()
    {
        isWin = true;
        playerAnimation.SetState(PlayerCharacterState.Win);
        ShowWinText();
        GameEnd();
    }

    private void GameEnd()
    {
        // Disable player movement and other actions
        DisableScene();
        ShowMainMenuButton();
    }

    // Disable player movement and other actions
    private void DisableScene()
    {

        DestroyAllBullets();

        DestroyAllCollectibles();

        AbilityManager abilityManager = FindObjectOfType<AbilityManager>();
        if (abilityManager != null)
        {
            abilityManager.enabled = false;
        }

        GravityController gravityController = FindObjectOfType<GravityController>();
        if (gravityController != null)
        {
            gravityController.enabled = false;
        }

        PlayerShooting playerShooting = FindObjectOfType<PlayerShooting>();
        if (playerShooting != null)
        {
            playerShooting.enabled = false;
        }

        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(false);
        }

        // Loop through each button in the array and disable it
        foreach (Button btn in actionButtons)
        {
            if (btn != null)
            {
                btn.interactable = false;
            }
        }
    }

    // Method to activate the Win text
    private void ShowWinText()
    {
        if (winText != null)
        {
            winText.gameObject.SetActive(true);
        }
    }

    // Method to activate the Lose text
    private void ShowLoseText()
    {
        if (loseText != null)
        {
            loseText.gameObject.SetActive(true);
        }
    }

    private void ShowMainMenuButton()
    {
        if (mainMenuButton != null)
        {
            mainMenuButton.gameObject.SetActive(true);
        }

    }

    private void DestroyAllBullets()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }

    private void DestroyAllCollectibles()
    {
        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Collectible");
        foreach (GameObject collectible in collectibles)
        {
            Destroy(collectible);
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Replace with your actual game scene name
    }

    public IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(0.917f);
        Destroy (playerCharacter);
        ShowLoseText();
        GameEnd();
    }
}
