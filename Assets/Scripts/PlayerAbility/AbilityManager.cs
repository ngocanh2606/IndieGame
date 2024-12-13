using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public enum AbilityType
    {
        GravityFree,
        GravityDash
    }

    [System.Serializable]
    public struct Ability
    {
        public AbilityType type;
        public Sprite sprite;

        //public UseAbility useAbility;
    }

    public Image abilityUIImage; //show icon for the ability
    public List<Ability> abilityDefinitions = new List<Ability>();
    private Stack<Ability> abilityStack = new Stack<Ability>(); // Stack to store abilities

    public GravityFreeAbility gravityFreeAbility;
    public DashAbility gravityDashAbility;

    //private bool activateSuccess = true;

    void Start()
    {
        gravityFreeAbility = GetComponent<GravityFreeAbility>();
        gravityDashAbility = GetComponent<DashAbility>();
    }
    public void OnAbilityButtonPressed()
    {
        Debug.Log("Pressed");
        if (abilityStack.Count > 0)
        {
            if(gravityFreeAbility.isGravityFree == false && gravityDashAbility.isDashing == false)
            {
                Ability currentAbility = abilityStack.Pop(); // Remove the top ability
                UseAbility(currentAbility.type);
                UpdateAbilityUI();
            }
            else
            {
                Debug.Log("Wait for cooldown!");
            }
        }
        else
        {
            Debug.Log("No abilities to use!");
        }
    }

    // Collect an ability and push it to the stack, is called when the power up is collected
    public void CollectAbility(AbilityType abilityType)
    {
        Ability? newAbility = abilityDefinitions.Find(a => a.type == abilityType);

        if (newAbility.HasValue)
        {
            abilityStack.Push(newAbility.Value);
            UpdateAbilityUI();
            Debug.Log("Added: " + abilityType);
        }
    }

    void UseAbility(AbilityType abilityType)
    {
        switch (abilityType)
        {
            case AbilityType.GravityFree:
                gravityFreeAbility.Activate();
                break;

            case AbilityType.GravityDash:
                gravityDashAbility.Activate();
                break;
        }
    }

    void UpdateAbilityUI()
    {
        if (abilityStack.Count > 0)
        {
            Ability topAbility = abilityStack.Peek();
            abilityUIImage.sprite = topAbility.sprite;
        }
        else
        {
            abilityUIImage.sprite = null; // Clear the sprite if no abilities are left
        }
    }
}