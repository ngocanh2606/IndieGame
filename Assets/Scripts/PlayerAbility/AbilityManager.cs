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
    public class Ability
    {
        public AbilityType type;
        public Sprite sprite;
    }

    public Image abilityUIImage; //show icon for the ability
    public List<Ability> abilityDefinitions = new List<Ability>();
    private Stack<Ability> abilityStack = new Stack<Ability>(); // Stack to store abilities

    public void OnAbilityButtonPressed()
    {
        if (abilityStack.Count > 0)
        {
            Ability currentAbility = abilityStack.Pop(); // Remove the top ability
            //currentAbility.Activate(); // Activate the ability
        }
        else
        {
            Debug.Log("No abilities to use!");
        }
    }

    // Collect an ability and push it to the stack, is called when the power up is collected
    public void CollectAbility(AbilityType abilityType)
    {
        Ability newAbility = abilityDefinitions.Find(a => a.type == abilityType);

        if (newAbility != null)
        {
            Debug.Log("2");

            abilityStack.Push(newAbility);
            Debug.Log("Added");
            UpdateAbilityUI();
        }
        else
        {
            Debug.Log("Ability not found for type: " + abilityType);
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
