using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    public void Fall()
    {
        animator.SetBool("IsFalling", true);
        Sequence fallSequence = DOTween.Sequence();
        fallSequence.Append(transform.DOPunchPosition(Vector3.left*0.4f, 1.5f,5,1f));
        fallSequence.Join(transform.DOPunchPosition(Vector3.up*0.4f, 1.5f,3,1f));
    }
}
