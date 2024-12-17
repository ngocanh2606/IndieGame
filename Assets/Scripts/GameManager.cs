using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winText;  
    [SerializeField] private TextMeshProUGUI loseText; 

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

        AbilityManager abilityManager = FindObjectOfType<AbilityManager>();
        if (abilityManager != null)
        {
            abilityManager.enabled = false;
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
}
