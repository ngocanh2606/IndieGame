using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //UI
    [SerializeField] private TextMeshProUGUI winText;  
    [SerializeField] private TextMeshProUGUI loseText; 
    [SerializeField] private Button mainMenuButton; 

    //To be disabled
    [SerializeField] private GameObject[] gameObjects; 
    [SerializeField] private Button[] actionButtons;

    //Run animation
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private GameObject playerCharacter;

    [SerializeField] private Rigidbody2D rb;

    // Method to call when the player loses
    public void Lose()
    {
        AudioManager.instance.PlayLoseSFX();
        playerAnimation.SetState(PlayerCharacterState.Die);
        StartCoroutine(WaitForDeathAnimation());
    }

    // Method to call when the player wins
    public void Win()
    {
        AudioManager.instance.PlayWinSFX();
        playerAnimation.SetState(PlayerCharacterState.Win);
        ShowWinText();
        GameEnd();
    }

    private void GameEnd()
    {
        // Disable player movement and other actions
        DisableScene();
        rb.AddForce(Vector2.down * 10, ForceMode2D.Force);
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

        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
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
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(0.917f);
        Destroy (playerCharacter);
        ShowLoseText();
        GameEnd();
    }
}