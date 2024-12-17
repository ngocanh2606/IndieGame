using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Import the TextMeshPro namespace
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Reference to the TextMeshProUGUI components for win/lose messages
    [SerializeField] private TextMeshProUGUI winText;   // Use TextMeshProUGUI instead of Text
    [SerializeField] private TextMeshProUGUI loseText;  // Use TextMeshProUGUI instead of Text

    [SerializeField] private Button[] actionButtons;
    [SerializeField] private GameObject player;

    // Method to call when the player loses
    public void Lose()
    {
        DisableScene();
        ShowLoseText();
    }

    // Method to call when the player wins
    public void Win()
    {
        DisableScene();
        ShowWinText();
    }

    // Disable player movement and other actions
    private void DisableScene()
    {
        player.SetActive(false);

        //PlayerController playerController = FindObjectOfType<PlayerController>();
        //if (playerController != null)
        //{
        //    playerController.enabled = false;
        //}

        //PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        //if (playerMovement != null)
        //{
        //    playerMovement.enabled = false;
        //}

        AbilityManager abilityManager = FindObjectOfType<AbilityManager>();
        if (abilityManager != null)
        {
            abilityManager.enabled = false;
        }

        //PlayerShooting playerShooting = FindObjectOfType<PlayerShooting>();
        //if (playerShooting != null)
        //{
        //    playerShooting.enabled = false;
        //}

        // Loop through each button in the array and disable it
        foreach (Button btn in actionButtons)
        {
            if (btn != null)
            {
                btn.interactable = false; // Disable the button interactability
                //btn.gameObject.SetActive(false); // Optionally, hide the button as well
            }
        }
    }

    // Method to show the "You Win!" message
    private void ShowWinText()
    {
        if (winText != null)
        {
            winText.gameObject.SetActive(true); // Activate the Win text
            winText.text = "You Win!"; // Set the text content
        }
    }

    // Method to show the "You Lose!" message
    private void ShowLoseText()
    {
        if (loseText != null)
        {
            loseText.gameObject.SetActive(true); // Activate the Lose text
            loseText.text = "You Lose!"; // Set the text content
        }
    }
}
