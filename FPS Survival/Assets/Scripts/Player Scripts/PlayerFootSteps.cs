using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{
    private AudioSource footStepsSound;
    private CharacterController characterController;
    [SerializeField] AudioClip[] footStepClip;

    [HideInInspector] public float minVolume, maxVolume;
    [HideInInspector] public float stepDistance;
    private float accumulatedDistance;
   
    void Awake()
    {
        footStepsSound = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayFootStepsSound();
    }

    private void PlayFootStepsSound()
    {
        if (!characterController.isGrounded)
            return;

        if(characterController.velocity.sqrMagnitude > 0)
        {
            // accumulated dist. is max. dist. we can move till footstep sound occurs
            accumulatedDistance += Time.deltaTime;
            if(accumulatedDistance > stepDistance)
            {
                footStepsSound.volume = Random.Range(minVolume, maxVolume);
                footStepsSound.clip = footStepClip[Random.Range(0, footStepClip.Length)];
                footStepsSound.Play();
                accumulatedDistance = 0f;
            }
        }
        else
        {
            accumulatedDistance = 0f;
        }
    }
}
