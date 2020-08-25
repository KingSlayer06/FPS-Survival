using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator animator;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Walk(bool walk)
    {
        animator.SetBool(AnimatorTags.WALK, walk);
    }

    public void Run(bool run)
    {
        animator.SetBool(AnimatorTags.RUN, run);
    }

    public void Attack()
    {
        animator.SetTrigger(AnimatorTags.ATTACK);
    }

    public void Death()
    {
        animator.SetTrigger(AnimatorTags.DEAD);
    }
}
