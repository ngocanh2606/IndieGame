using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    private Stack<Ability> abilityStack = new Stack<Ability>(); // Stack to store abilities

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Replace with your ability button input
        {
            UseAbility();
        }
    }

    // Collect an ability and push it to the stack
    public void CollectAbility(Ability newAbility)
    {
        Debug.Log("Collected Ability: " + newAbility.abilityName);
        abilityStack.Push(newAbility);
    }

    // Use the most recently collected ability
    private void UseAbility()
    {
        if (abilityStack.Count > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            Ability currentAbility = abilityStack.Pop(); // Remove the top ability
            currentAbility.Activate(); // Activate the ability
        }
        else
        {
            Debug.Log("No abilities to use!");
        }
    }
}
