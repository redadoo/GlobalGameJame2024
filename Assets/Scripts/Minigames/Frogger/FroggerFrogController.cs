using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerFrogController : MonoBehaviour
{
    [SerializeField] private Animator animator;


    public void JumpUp()
    {
        animator.SetBool("IsGrounded", false);
        animator.SetFloat("VerticalSpeed", 1f);
    }

    public void JumpDown()
    {
        animator.SetBool("IsGrounded", false);
        animator.SetFloat("VerticalSpeed", -1f);
    }

    public void Idle()
    {
        animator.SetBool("IsGrounded", true);
        animator.SetFloat("VerticalSpeed", 0f);
    }
}
