using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChanging : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement pm;
    private bool isMovement;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
        pm = this.GetComponent<PlayerMovement>();
        isMovement = pm.IsMovement();
        AnimatorEnabled(false);
    }

    void Update()
    {
        isMovement = pm.IsMovement();
        if (isMovement)
        {
            AnimatorEnabled(true);
        }
        else
        {
            AnimatorEnabled(false);
        }
    }

    private void AnimatorEnabled(bool n)
    {
        animator.enabled = n;
    }
}
