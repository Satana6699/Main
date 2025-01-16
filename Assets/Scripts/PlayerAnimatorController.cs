using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMove playerMove;

    void Update()
    {
        animator.SetFloat("Speed", playerMove.Speed);
        animator.SetBool("Jump", playerMove.IsJumpForAnimation);
        animator.SetBool("Grounded", playerMove.IsGroundedForAnimation);
        animator.SetBool("FreeFall", playerMove.IsFreeFallForAnimation);
    }
}
