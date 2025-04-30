using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    public TutorialStep currentStep;

    public GameObject[] tutorialPopups; // each popup UI for a step
    private bool stepInProgress = false;
    private bool tutorialInProgress = false;

    [SerializeField] private float[] popupTime;

    private int commonCount;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] boss;

    //Reference to other scripts
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Checkpoint checkpoint;
    [SerializeField] private AbilityManager abilityManager;
    [SerializeField] private GravityFreeAbility gravityFreeAbility;
    [SerializeField] private DashAbility dashAbility;
    [SerializeField] private GravityController gravityController;
    [SerializeField] private BossController bossController;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        currentStep = 0;
        foreach (GameObject b in boss) {
            b.SetActive(false);
        }
        StartCoroutine(TutorialWithPopup());
    }

    void Update()
    {
        if (tutorialInProgress) return;

        switch (currentStep)
        {
            case TutorialStep.Run:
                if (playerController.moveLeft || playerController.moveRight)
                {
                    AdvanceToNextStep();
                }
                break;

            case TutorialStep.Jump:
                if (playerMovement.isJumping == true)
                {
                    AdvanceToNextStep();
                }
                break;

            case TutorialStep.ReachCheckpoint:
                checkpoint.go.SetActive(true);
                if (checkpoint.reached)
                {
                    AdvanceToNextStep();
                }
                break;

            case TutorialStep.CollectAbility:
                if (abilityManager.abilityStack.Count > 0)
                {
                    AdvanceToNextStep();
                }
                break;

            case TutorialStep.ExplainAbilityUI:
                AdvanceToNextStep();
                break;

            case TutorialStep.UseAbility:
                if (commonCount == 0)
                {
                    if (gravityFreeAbility.isGravityFree)
                    {
                        commonCount = 1;
                        currentStep = TutorialStep.ExplainGravityFree;
                        stepInProgress = false;
                        break;
                    }
                    else if (abilityManager.dashAttempted)
                    {
                        commonCount = 2;
                        currentStep = TutorialStep.ExplainDash;
                        stepInProgress = false;
                        break;
                    }
                }
                else if (commonCount == 1)
                {
                    if (abilityManager.dashAttempted)
                    {
                        currentStep = TutorialStep.ExplainDash;
                        stepInProgress = false;
                        commonCount = 3;
                        break;
                    }
                }
                else if (commonCount == 2)
                {
                    if (gravityFreeAbility.isGravityFree)
                    {
                        currentStep = TutorialStep.ExplainGravityFree;
                        stepInProgress = false;
                        commonCount = 3;
                        break;
                    }
                }

                break;

            case TutorialStep.ExplainGravityFree:
                if(commonCount == 3)
                {
                    AdvanceToNextStep();
                }
                else
                {
                    currentStep = TutorialStep.UseAbility;
                    stepInProgress = false;
                }
                break;

            case TutorialStep.ExplainDash:
                if (commonCount == 3)
                {
                    AdvanceToNextStep();
                }
                else
                {
                    currentStep = TutorialStep.UseAbility;
                    stepInProgress = false;
                }
                break;

            case TutorialStep.GravityTraining:
                if (commonCount == 0)
                {
                    gravityController.StartGravityShift();
                    commonCount = 1;
                }
                if (commonCount == 1 && gravityController.countDirection == 4)
                {
                    gravityController.playerRigidbody.AddForce(10*Vector2.down, ForceMode2D.Force);
                    commonCount = 2;
                }
                break;

            case TutorialStep.BossIntro:
                foreach (GameObject b in boss)
                {
                    b.SetActive(true);
                }
                AdvanceToNextStep();
                break;

            case TutorialStep.Shoot:
                if (bossController.currentHealth < bossController.maxHealth)
                {
                    AdvanceToNextStep();
                }
                break;

            case TutorialStep.ExplainBoss:
                AdvanceToNextStep();
                break;

            case TutorialStep.KillBoss:
                stepInProgress = false;
                break;

            case TutorialStep.LoseRestart:
                currentStep = TutorialStep.KillBoss;
                break;
        }

        if (stepInProgress) return;

        StartCoroutine(TutorialWithPopup());
    }

    private IEnumerator TutorialWithPopup()
    {
        stepInProgress = true;
        tutorialInProgress = true;
        // Show the correct popup
        foreach (GameObject popup in tutorialPopups)
            popup.SetActive(false);

        tutorialPopups[(int)currentStep].SetActive(true);

        yield return new WaitForSeconds(popupTime[(int)currentStep]);

        tutorialInProgress = false;
    }

    public void AdvanceToNextStep()
    {
        currentStep++;
        commonCount = 0;
        stepInProgress = false;
    }
}
