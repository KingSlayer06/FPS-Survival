using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip screamClip, dieClip;
    [SerializeField] private AudioClip[] attackClips;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ScreamSound()
    {
        audioSource.clip = screamClip;
        audioSource.Play();
    }

    public void DieSound()
    {
        audioSource.clip = dieClip;
        audioSource.Play();
    }

    public void AttackSound()
    {
        audioSource.clip = attackClips[Random.Range(0, attackClips.Length)];
        audioSource.Play();
    }
}
