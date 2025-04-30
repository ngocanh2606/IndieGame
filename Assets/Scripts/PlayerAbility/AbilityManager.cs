using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    // Enum to define different types of abilities
    public enum AbilityType
    {
        GravityFree,
        GravityDash
    }

    // Struct to store ability data, including its type and icon sprite
    [System.Serializable]
    public struct Ability
    {
        public AbilityType type;  
        public Sprite sprite; 
    }

    private PlayerAnimation playerAnimation;

    public Image abilityUIImage;                                   // UI Image component to display the ability icon
    public List<Ability> abilityDefinitions = new List<Ability>(); // List of defined abilities (collected abilities)
    public Stack<Ability> abilityStack = new Stack<Ability>();    // Stack to store abilities that the player has collected

    // References to the actual ability scripts for GravityFree and Dash
    public GravityFreeAbility gravityFreeAbility;
    public DashAbility gravityDashAbility;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Start()
    {
        gravityFreeAbility = GetComponent<GravityFreeAbility>();
        gravityDashAbility = GetComponent<DashAbility>();       
    }

    // Method called when the ability button is pressed (UI event)
    public void OnAbilityButtonPressed()
    {
        // If the ability stack has abilities, proceed to use one
        if (abilityStack.Count > 0)
        {
            // Check that no gravity or dash is in progress
            if (gravityFreeAbility.isGravityFree == false && gravityDashAbility.isDashing == false)
            {
                AudioManager.instance.PlayUseAbilitySFX();
                Ability currentAbility = abilityStack.Pop(); // Pop the top ability from the stack
                UseAbility(currentAbility.type);             // Use the selected ability
                UpdateAbilityUI();
            }
        }
    }

    // Collect an ability and push it to the stack (called when the player picks up an ability)
    public void CollectAbility(AbilityType abilityType)
    {
        // Find the ability definition in the list based on the provided abilityType
        Ability? newAbility = abilityDefinitions.Find(a => a.type == abilityType);

        // If the ability exists, add it to the stack and update the UI
        if (newAbility.HasValue)
        {
            abilityStack.Push(newAbility.Value);  
            UpdateAbilityUI();                    
        }
    }

    // Method to activate the selected ability based on its type
    void UseAbility(AbilityType abilityType)
    {
        // Use a switch statement to trigger the corresponding ability
        switch (abilityType)
        {
            case AbilityType.GravityFree:
                gravityFreeAbility.Activate();
                break;

            case AbilityType.GravityDash:
                PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
                if(playerMovement.moveDirection.x != 0)
                {
                    playerAnimation.SetState(PlayerCharacterState.Dash);
                    gravityDashAbility.Activate(playerMovement.moveDirection);
                }
                break;
        }
    }

    // Update the UI to show the current ability's icon or clear it if no abilities are available
    void UpdateAbilityUI()
    {
        if (abilityStack.Count > 0)
        {
            Ability topAbility = abilityStack.Peek();
            abilityUIImage.sprite = topAbility.sprite;
        }
        else
        {
            abilityUIImage.sprite = null;
        }
    }
}
