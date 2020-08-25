using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private CharacterController characterController;
    private PlayerFootSteps playerFootSteps;
    private PlayerStats playerStats;

    private bool isCrouching = false;

    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float crouchSpeed = 1.5f;
    [SerializeField] private float sprintThreshold = 20f;

    [SerializeField] private float characterControllerStandHeight = 1.8f;
    [SerializeField] private float characterControllerCrouchHeight = 0.9f;

    private float minSprintVolume = 0.7f;
    private float maxSprintVolume = 1f;
    private float crouchVolume = 0.1f;
    private float minWalkVolume = 0.2f;
    private float maxWalkVolume = 0.6f;
    private float walkStepDistance = 0.4f;
    private float sprintStepDistance = 0.25f;
    private float crouchStepDistance = 0.5f;
    private float sprintValue = 100f;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        characterController = GetComponent<CharacterController>();
        playerFootSteps = GetComponentInChildren<PlayerFootSteps>();
        playerStats = GetComponent<PlayerStats>();

        playerFootSteps.minVolume = minWalkVolume;
        playerFootSteps.maxVolume = maxWalkVolume;
        playerFootSteps.stepDistance = walkStepDistance;
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }

    private void Sprint()
    {
        // If we have stamina
        if(sprintValue > 0f)
        {
            if (Input.GetKeyDown(Keycode.LEFT_SHIFT) && !isCrouching)
            {
                playerMovement.speed = sprintSpeed;

                playerFootSteps.stepDistance = sprintStepDistance;
                playerFootSteps.minVolume = minSprintVolume;
                playerFootSteps.maxVolume = maxSprintVolume;
            }
        }

        if (Input.GetKeyUp(Keycode.LEFT_SHIFT) && !isCrouching)
        {
            playerMovement.speed = moveSpeed;

            playerFootSteps.stepDistance = walkStepDistance;
            playerFootSteps.minVolume = minWalkVolume;
            playerFootSteps.maxVolume = maxWalkVolume;
        }

        // Decrease stamina while sprinting
        if (Input.GetKey(Keycode.LEFT_SHIFT) && !isCrouching)
        {
            sprintValue -= sprintThreshold * Time.deltaTime;
            if (sprintValue < 0f)
            {
                sprintValue = 0f;
                playerMovement.speed = moveSpeed;

                playerFootSteps.stepDistance = walkStepDistance;
                playerFootSteps.minVolume = minWalkVolume;
                playerFootSteps.maxVolume = maxWalkVolume;
            }
        } 
        else
        {
            if(sprintValue != 100f)
            {
                sprintValue += (sprintThreshold / 2) * Time.deltaTime;
                if(sprintValue > 100f)
                {
                    sprintValue = 100f;
                }
            }
        }
        playerStats.DisplayStamina(sprintValue);
    }

    private void Crouch()
    {
        if(Input.GetKeyDown(Keycode.LEFT_CTRL))
        {
            if(isCrouching)
            {

                characterController.height = characterControllerStandHeight;
                playerMovement.speed = moveSpeed;
                isCrouching = false;

                playerFootSteps.stepDistance = walkStepDistance;
                playerFootSteps.minVolume = minWalkVolume;
                playerFootSteps.maxVolume = maxWalkVolume;
            }
            else
            {
                characterController.height = characterControllerCrouchHeight;
                playerMovement.speed = crouchSpeed;
                isCrouching = true;

                playerFootSteps.stepDistance = crouchStepDistance;
                playerFootSteps.minVolume = playerFootSteps.maxVolume = crouchVolume;
            }
        }
    }
}
