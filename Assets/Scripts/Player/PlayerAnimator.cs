using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer playerSpriteRenderer;

    public bool Flip
    {
        get
        {
            return playerSpriteRenderer.flipX;
        }
        set
        {
            playerSpriteRenderer.flipX = value;
        }
    }
    public float ParameterSpeed { get { return animator.GetFloat("Speed"); } set { animator.SetFloat("Speed", value); } }
    public bool IsMoving  { get { return animator.GetBool("IsMoving"); } set { animator.SetBool("IsMoving", value); } }
}
